using FileManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ViewModels
{
    public class FolderPropertiesViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private Folder _currentFolder;
        public Folder CurrentFolder { get { return _currentFolder; } set { _currentFolder = value; OnPropertyChanged("CurrentFile"); } }

        public FolderPropertiesViewModel(Folder currentFolder)
        {
            _currentFolder = currentFolder;
        }
    }
}
