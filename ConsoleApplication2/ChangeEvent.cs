using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    //[Table]
    //[InheritanceMapping(Type = typeof(PhoneBaseEventChange), IsDefault = true, Code = 1)]
    abstract class ChangeEvent
    {
        //[Column(IsDiscriminator = true)]
        //int randomkey { get; set; }
        public abstract DateTime getTransactionTime();
    }
}
