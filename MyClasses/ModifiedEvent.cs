using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.MyClasses
{
    internal class ModifiedEvent
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Title { get; set; }
        public string Equipment { get; set; }
        public List<AdditionalFile> AdditionalMaterials { get; set; }
        public string Hall { get; set; }
        public string SendingUser { get; set; }
    }
}
