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
    public class FilePropertiesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private File _currentFile;
        public File CurrentFile { get { return _currentFile; } set { _currentFile = value;  OnPropertyChanged("CurrentFile"); } }

        public FilePropertiesViewModel(File currentFile)
        {
            _currentFile = currentFile;
        }
    }
}
