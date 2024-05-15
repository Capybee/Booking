using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.MyClasses
{
    public class AdditionalFile
    {
		private string _FileName;
		public string FileName
		{
			get { return _FileName; }
			set { _FileName = value; }
		}

		private string _FilePath;
		public string FilePath
		{
			get { return _FilePath; }
			set { _FilePath = value; }
		}

	}
}
