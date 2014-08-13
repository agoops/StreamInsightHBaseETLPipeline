using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{

    abstract class ChangeEvent
    {
        
        public abstract DateTime getTransactionTime();
    }
}
