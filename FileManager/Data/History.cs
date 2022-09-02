using System;
using System.Collections.Generic;

namespace FileManager.Data
{
    public partial class History
    {
        public int Id { get; set; }
        public string Filename { get; set; } = null!;
        public DateTime DateVisited { get; set; }
    }
}
