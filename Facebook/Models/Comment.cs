using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
		public string userName { get; set; }

		public string userID { get; set; }
		[ForeignKey("userID")]
		public virtual ApplicationUser user { get; set; }

		public int photoID { get; set; }
		[ForeignKey("photoID")]
		public virtual Photo photo { get; set; }

	}
}