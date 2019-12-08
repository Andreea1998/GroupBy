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
        [Required(ErrorMessage = "You must add your name")]
        [StringLength(25, ErrorMessage = "Your name cannot be longer than 25 characters")]
        public string name { get; set; }
        [Required(ErrorMessage = "You must add your description")]
        public string about { get; set; }
        [Required(ErrorMessage = "You must add your birthdate")]
        public DateTime birthday { get; set; }
        public string hobbies { get; set; }
        public bool active { get; set; }
        public bool privateP { get; set; }

		public string userID { get; set; }
        [Required]
		public virtual ApplicationUser user { get; set; }
	}
}