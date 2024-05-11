using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.MyClasses
{
    public class UsersCollection
    {
		private int _Id;
		public int Id
		{
			get { return _Id; }
			set { _Id = value; }
		}

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

		private string _Login;
		public string Login
		{
			get { return _Login; }
			set { _Login = value; }
		}

		private string _Password;
		public string Password
		{
			get { return _Password; }
			set { _Password = value; }
		}

		private string _Post;
		public string Post
		{
			get { return _Post; }
			set { _Post = value; }
		}

		public UsersCollection(int Id,string FIO, string Role, string Login, string Password, string Post)
		{
			_Id = Id;
			_FIO = FIO;
			_Role = Role;
			_Login = Login;
			_Password = Password;
			_Post = Post;
		}
	}
}
