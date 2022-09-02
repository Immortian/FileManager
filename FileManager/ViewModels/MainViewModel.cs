using FileManager.Data;
using FileManager.Models;
using FileManager.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FileManager.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        //Directory navigation

        /// <summary>
        /// path for current folder
        /// </summary>
        private string _path;
        public string Path
        {
            get { return _path; }
            set { _temppath = _path; _path = value; CDCommand(_path); OnPropertyChanged("Items"); OnPropertyChanged("Path"); }
        }

        /// <summary>
        /// last correct used path
        /// </summary>
        private string _temppath;

        /// <summary>
        /// Set path to base directory, get db context from dependency injection
        /// </summary>
        public MainViewModel(historydbContext context)
        {
            Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            _context = context;
        }

        /// <summary>
        /// separeted path for current folder
        /// </summary>
        private string[] _pathMembers;
        public string[] PathMembers
        {
            get
            {
                return _pathMembers
                ?? (_pathMembers = SplitPath(Path));
            }
            set { _pathMembers = value; OnPropertyChanged("PathMembers"); }
        }

        /// <summary>
        /// Separete path parts to string array
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string[] SplitPath(string path)
        {
            string[] split = Path.Split('\\');
            for (int i = 0; i < split.Length; i++)
            {
                split[i] += "\\";
            }
            return split;
        }

        /// <summary>
        /// command to go up the directory tree
        /// </summary>
        private RelayCommand _goUpCommand;
        public RelayCommand GoUpCommand =>
            _goUpCommand
            ?? (_goUpCommand = new RelayCommand(obj =>
                {
                    GoUp();
                }, obj => Path != Path.Split('\\').First() + "\\"));

        /// <summary>
        /// Removes last part of path to go up the directory tree
        /// </summary>
        private void GoUp()
        {
            Path = Path.Replace("\\" + Path.Split('\\').Last(), "");
            if (Path.Split('\\').Length == 1)
                Path = Path + "\\";
        }

        //Item collection

        /// <summary>
        /// Collection of items in current directory
        /// </summary>
        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items { get { return _items; } private set { _items = value; } }

        /// <summary>
        /// Update collection of items while changing directory
        /// </summary>
        /// <param name="path"></param>
        private void CDCommand(string path)
        {
            var tempItems = Items;
            if (EnviromentProvider.TryGetItemsInDirectory(path, out tempItems))
                Items = tempItems;
            else
            {
                MessageBox.Show("This path does not exists, or permission denied. Try to restart as administrator");
                Path = _temppath;
            }
               
        }

        /// <summary>
        /// logging db context
        /// </summary>
        private readonly historydbContext _context;

        //Item commands

        /// <summary>
        /// command to open file or folder
        /// </summary>
        private RelayCommand _executeCommand;
        public RelayCommand ExecuteCommand
        {
            get
            {
                return _executeCommand
                    ?? (_executeCommand = new RelayCommand(obj =>
                    {
                        Execute((Item)obj);
                    }));
            }
        }

        /// <summary>
        /// if item is folder: change current directory path,
        /// file: trying to open it
        /// </summary>
        /// <param name="item"></param>
        private void Execute(Item item)
        {
            if (item.Type == ItemType.folder)
            {
                Path = item.Path;
            }
            else
            {
                //Process.Start(item.Path);
                var p = new Process();
                p.StartInfo = new ProcessStartInfo(item.Path)
                {
                    UseShellExecute = true
                };
                p.Start();
                try
                {
                    _context.Histories.Add(new History
                    {
                        Filename = item.Path,
                        DateVisited = DateTime.Now
                    });
                    _context.SaveChanges();
                }
                catch(Exception e)
                {
                    MessageBox.Show("Check your db connection");
                }
            }
        }

        
        /// <summary>
        /// Properties page for selected item
        /// </summary>
        private Page _displayPage;
        public Page DisplayPage { get { return _displayPage; } set { _displayPage = value; OnPropertyChanged("DisplayPage"); } }

        /// <summary>
        /// command to update properties page
        /// </summary>
        private RelayCommand _showPropertiesCommand;
        public RelayCommand ShowPropertiesCommand
        {
            get
            {
                return _showPropertiesCommand
                    ?? (_showPropertiesCommand = new RelayCommand(obj =>
                    {
                        ShowProperties((Item)obj);
                    }));
            }
        }

        /// <summary>
        /// Create view models and represents property pages
        /// </summary>
        /// <param name="item"></param>
        private void ShowProperties(Item item)
        {
            if (item.Type == ItemType.folder)
            {
                FolderPropertiesViewModel propsVM = new FolderPropertiesViewModel((Folder)item);
                DisplayPage = new FolderPropertiesPage(propsVM);
            }
            else
            {
                FilePropertiesViewModel propsVM = new FilePropertiesViewModel((Models.File)item);
                DisplayPage = new FilePropertiesPage(propsVM);
            }
        }
    }
}
