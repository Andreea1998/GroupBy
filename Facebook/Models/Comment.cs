using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
	public class Comment
	{
		[Key]
		public int commentID { get; set; }
		[Required]
		public string text { get; set; }
		public int nrOfLikes { get; set; }

		public string userID { get; set; }
		[Required]
		public virtual ApplicationUser user { get; set; }

		public int photoID { get; set; }
		[Required]
		public virtual Photo photo { get; set; }

	}
}