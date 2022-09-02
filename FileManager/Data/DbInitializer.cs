using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Data
{
    public static class DbInitializer
    {
        public static void Initialize(historydbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
