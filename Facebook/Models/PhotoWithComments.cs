using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
	public class PhotoWithComments
	{
		public Photo photo { get; set; }
		public IEnumerable<Comment> comments { get; set; }
	}
}