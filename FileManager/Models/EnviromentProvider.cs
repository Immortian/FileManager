using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager.Models
{
    public static class EnviromentProvider
    {
        /// <summary>
        /// Searh for folders and files in directory
        /// </summary>
        /// <param name="Path"></param>
        /// <returns>bool result and ObservableCollection of Files and Folders downcasted to Items</returns>
        /// <exception cref="Exception"></exception>
        public static bool TryGetItemsInDirectory(string Path, out ObservableCollection<Item> Items)
        {
            DirectoryInfo di = new DirectoryInfo(Path);
            try
            {
                if (!di.Exists)
                    throw new Exception("Path is not exsist");

                ObservableCollection<Item> items = new ObservableCollection<Item>();
            
                foreach (var dir in di.GetDirectories())
                {
                    if((dir.Attributes & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint &&
                    (dir.Attributes & FileAttributes.System) != FileAttributes.System)
                        items.Add(new Folder
                        {
                            Name = dir.Name,
                            Path = dir.FullName,
                            Size = dir.GetFiles().Sum(x => x.Length),
                            AmountOfItems = dir.GetFiles().Length,
                            Type = ItemType.folder
                        });
                }

                foreach (var file in di.GetFiles())
                {
                    if ((di.Attributes & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint &&
                    (di.Attributes & FileAttributes.System) != FileAttributes.System)
                        items.Add(new File
                        {
                            Name = file.Name,
                            Path = file.FullName,
                            Size = file.Length,
                            DateCreated = file.CreationTime,
                            DateModified = file.LastWriteTime,
                            Type = GetType(file.FullName)
                        });
                }
                Items = items;
                return true;
            }
            catch (Exception e)
            {
                Items = new ObservableCollection<Item>();
                return false;
            }

        }

        private static ItemType? GetType(string fileName)
        {
            var ext = fileName.Split('.').Last();
            if (ext == "doc" || ext == "docx")
                return ItemType.doc;
            else if (ext == "xml" || ext == "xls" || ext == "xlsx")
                return ItemType.spreadsheet;
            else if (ext == "pdf")
                return ItemType.pdf;
            else if (ext == "url")
                return ItemType.label;
            else if (ext == "png" || ext == "jpg" || ext == "jpeg")
                return ItemType.image;
            else 
                return null;
        }
    }
}
