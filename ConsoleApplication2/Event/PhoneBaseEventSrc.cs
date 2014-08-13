using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    //[Table]
    //[InheritanceMapping(Type = typeof(PhoneBaseEventChange), IsDefault = true, Code = 1)]
    class PhoneBaseEventSrc : SourceEvent
    {
        //[Column(IsPrimaryKey = true)]
        public Guid? ActivityId{get; set;}

	    public int? mbs_phonestatus {get; set;}

        //[Column(IsDiscriminator = true)]
	    public string mbs_gsxactivityid  {get; set;}
	    public DateTime? mcs_dialstarttime  {get; set;}
	    public string mcs_Duration  {get; set;}
	    public string mcs_HoldTime {get; set;}
    }
}
