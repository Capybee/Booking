using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.MyClasses
{
    public class UsersCollection
    {
		private string _FIO;
		public string FIO
		{
			get { return _FIO; }
			set { _FIO = value; }
		}

		private string _Role;
		public string Role
		{
			get { return _Role; }
			set { _Role = value; }
		}

		public UsersCollection(string FIO, string Role)
		{
			_FIO = FIO;
			_Role = Role;
		}
	}
}
