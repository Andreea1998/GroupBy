using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
    public class Profile
    {
        [Key]
        public int profileID { get; set; }
        public string profileImageUrl { get; set; }
        [Required]
        public string name { get; set; }
        public string about { get; set; }
        [Required]
        public DateTime birthday { get; set; }
        public string hobbies { get; set; }
        public bool active { get; set; }
        public bool privateP { get; set; }

		public string userID { get; set; }
		[Required]
		public virtual ApplicationUser user { get; set; }
	}
}