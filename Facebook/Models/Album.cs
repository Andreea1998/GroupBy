using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
	public class Album
	{
		[Key]
		public int albumID { get; set; }
		[Required]
		public string name { get; set; }


		public int userID { get; set; }
		[Required]
		public virtual ApplicationUser user { get; set; }
		public virtual ICollection<Photo> photos { get; set; }
	}
}