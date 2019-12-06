using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
	public class Message
	{
		[Key]
		public int messageID { get; set; }
		public string text { get; set; }

		public int groupID { get; set; }
		[Required]
		public virtual Group group { get; set; }
		
		public string userID { get; set; }
		[Required]
		public virtual ApplicationUser user { get; set; }
	}
}