using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
    public class Profile
    {
        [Key]
        public int profileID { get; set; }

        public string profileImageUrl { get; set; }
        public string backgroundImageUrl { get; set; }
        [Required(ErrorMessage = "You must add your name")]
        [StringLength(25, ErrorMessage = "Your name cannot be longer than 25 characters")]
        public string name { get; set; }
        [Required(ErrorMessage = "You must add your description")]
        public string about { get; set; }
        public string occupation { get; set; }
        public string studies { get; set; }
        public string languages { get; set; }
        public string job { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string web { get; set; }
        public bool active { get; set; }
        public bool privateP { get; set; }

		
		public string userId { get; set; } //User ID
		[ForeignKey("userId")]
		public virtual ApplicationUser user { get; set; }
	}
}