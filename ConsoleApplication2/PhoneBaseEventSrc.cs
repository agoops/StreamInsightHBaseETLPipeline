using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class PhoneBaseEventSrc : SourceEvent
    {
        public Guid? ActivityId{get; set;}
	    public int? mbs_phonestatus {get; set;}
	    public string mbs_gsxactivityid  {get; set;}
	    public DateTime? mcs_dialstarttime  {get; set;}
	    public string mcs_Duration  {get; set;}
	    public string mcs_HoldTime {get; set;}
    }
}
