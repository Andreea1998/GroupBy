using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
	public class Photo
	{
		[Key]
		public int photoID { get; set; }
		[Required(ErrorMessage = "Please add an URL for your photo")]
		public string photoURL { get; set; }
		public int nrOfLikes { get; set; }

		public int albumID { get; set; }
		[Required]
		public virtual Album album { get; set; }

		public virtual ICollection<Comment> comments { get; set; }
	}
}