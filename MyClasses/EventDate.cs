using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.MyClasses
{
    public class EventDate
    {
		private DateTime _Start;
		public DateTime Start
		{
			get { return _Start; }
			set { _Start = value; }
		}

		public List<TimeSpan> StartTime = new List<TimeSpan>()
		{
			new TimeSpan(8,0,0), new TimeSpan(8,30,0), new TimeSpan(9,0,0), new TimeSpan (9,30,0), new TimeSpan(10,0,0),
			new TimeSpan(10,30,0), new TimeSpan(11,0,0), new TimeSpan(11,30,0),	new TimeSpan(12,0,0), new TimeSpan(12,30,0),
			new TimeSpan(13,0,0), new TimeSpan(13,30,0), new TimeSpan(14,0,0), new TimeSpan(14,30,0), new TimeSpan(15,0,0),
			new TimeSpan(15,30,0),new TimeSpan(16,0,0),new TimeSpan(16,30,0), new TimeSpan(17,0,0), new TimeSpan(17,30,0),
			new TimeSpan(18,0,0)
		};

	}
}
