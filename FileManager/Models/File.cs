using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Models
{
    public class File : Item
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
