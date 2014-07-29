using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    interface ChangeEvent
    {
        public int Operation { get; set; }
        public DateTime TransactionTime { get; set; }
    }
}
