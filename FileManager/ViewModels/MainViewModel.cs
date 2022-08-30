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
            set { _path = value; FinalLoad(); OnPropertyChanged("Items"); }
        }
        //private readonly ObservableCollection<Item> _items;
        //public ObservableCollection<Item> Items { get { return _items; } }

        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items { get; set; }

        public MainViewModel()
        {
            Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        public void FinalLoad()
        {
            Items = EnviromentProvider.GetItemsInDirectory(Path);
        }
    }
}
