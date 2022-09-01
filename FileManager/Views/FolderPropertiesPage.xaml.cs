using FileManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileManager.Views
{
    /// <summary>
    /// Логика взаимодействия для FolderPropertiesPage.xaml
    /// </summary>
    public partial class FolderPropertiesPage : Page
    {
        public FolderPropertiesPage(FolderPropertiesViewModel VM)
        {
            InitializeComponent();
            this.DataContext = VM;
        }
    }
}
