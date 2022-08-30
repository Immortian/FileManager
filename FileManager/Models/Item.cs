using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Models
{
    public class Item
    {
        public string Path { get; set; }
        public long Size { get; set; }
        public string Name { get; set; }
        public ItemType? Type { get; set; }
        public string Icon 
        {
            get 
            {
                if (Type == ItemType.folder)
                    return @"/FileManager;component/Icons/folder.png";
                else if (Type == ItemType.doc)
                    return @"/FileManager;component/Icons/docs.png";
                else if (Type == ItemType.spreadsheet)
                    return @"/FileManager;component/Icons/spreadsheet.png";
                else if (Type == ItemType.pdf)
                    return @"/FileManager;component/Icons/pdf.png";
                else if (Type == ItemType.image)
                    return @"/FileManager;component/Icons/image.png";
                else if (Type == ItemType.label)
                    return @"/FileManager;component/Icons/label.png";
                else
                    return @"/FileManager;component/Icons/unknown.png";
            }
        }
}
    public enum ItemType
    {
        folder,
        doc,
        spreadsheet,
        image,
        pdf,
        label
    }
}
