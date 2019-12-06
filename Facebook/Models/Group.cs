using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
	public class Group
	{
		[Key]
		public int groupID { get; set; }
		public string name { get; set; }

		public virtual ICollection<ApplicationUser> users { get; set; }
		public virtual ICollection<Message> messages { get; set; }

	}
}