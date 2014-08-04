using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class OwnerBaseEventSrc : SourceEvent
    {
        public int? OwnerIdType { get; set; }
        public Guid? OwnerId { get; set; }
        public string Name { get; set; }
        public Byte[] VersionNumber { get; set; }
        public string YomiName { get; set; }
    }
}
