using FileManager.Models;
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

        private string _path;
        public string Path
        {
            get { return _path; }
            set { _path = value; ChangeDirectoryCommand(_path); OnPropertyChanged("Items"); }
        }

        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items { get { return _items; } private set { _items = value; } }

        public MainViewModel()
        {
            Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private RelayCommand _executeCommand;
        public RelayCommand ExecuteCommand
        {
            get
            {
                return _executeCommand
                    ?? (_executeCommand = new RelayCommand(obj =>
                    {
                        Execute((Item)obj);
                        //ChangeDirectoryCommand(@"C:/Users/andrey/Downloads");
                    }));
            }
        }
        private void ChangeDirectoryCommand(string path)
        {
            Items = EnviromentProvider.GetItemsInDirectory(path);
            OnPropertyChanged("Path");
            OnPropertyChanged("Items");
        }

        private void Execute(Item item)
        {
            if (item.Type == ItemType.folder)
            {
                Path = item.Path;
                OnPropertyChanged("Path");
                OnPropertyChanged("Items");
            }
            else
            {
                Process.Start(item.Path);
                //Write in DB
            }
        }
    }
}
