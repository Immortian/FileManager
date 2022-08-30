using FileManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
            set { _path = value; }
        }
        //private readonly ObservableCollection<Item> _items;
        //public ObservableCollection<Item> Items { get { return _items; } }

        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items { get; set; }

        public MainViewModel()
        {
            TestLoad();
        }
        public void TestLoad()
        {
            Items = new ObservableCollection<Item>();
            Items.Add(new File
            {
                Name = "someFile",
                Path = @"C:\Users\andrey\Desktop\file.exe",
                Size = 12,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            });
            Items.Add(new Folder
            {
                Name = "someFolder",
                Path = @"C:\Users\andrey\Desktop\",
                Type = ItemType.folder,
                Size = 120,
                AmountOfItems = 2
            });
        }
    }
}
