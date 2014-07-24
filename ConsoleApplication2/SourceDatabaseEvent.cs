using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class SourceDatabaseEvent : SourceEvent
    {
        public int SaleID { get; set; }
        public string Product { get; set; }
        public DateTime SaleDate { get; set; }
        public int StatusID { get; set; }
        public decimal SalePrice { get; set; }

        public string getPrimaryKey()
        {
            return "SaleID";
        }
    }
}
