using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
	public class Message
	{
		[Key]
		public int messageID { get; set; }
		public string text { get; set; }
        public DateTime date { get; set; }
        public string userName { get; set; }

		public int groupID { get; set; }
		[ForeignKey("groupID")]
		public virtual Group group { get; set; }
		
		public string userID { get; set; }
        [ForeignKey("userID")]
        public virtual ApplicationUser user { get; set; }
	}
}