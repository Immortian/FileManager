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
        /// <returns>ObservableCollection of Files and Folders downcasted to Items</returns>
        /// <exception cref="Exception"></exception>
        public static ObservableCollection<Item> GetItemsInDirectory(string Path)
        {
            DirectoryInfo di = new DirectoryInfo(Path);

            if (!di.Exists)
                throw new Exception("Path is not exsist");

            ObservableCollection<Item> items = new ObservableCollection<Item>();
            try
            {
                foreach (var dir in di.GetDirectories())
                {
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
            }
            catch(Exception e) { }
            return items;
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
