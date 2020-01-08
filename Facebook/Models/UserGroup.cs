using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
    public class UserGroup
    {
        [Key, Column(Order = 0)]
        public string userId { get; set; }
        [Key, Column(Order = 1)]
        public int groupId { get; set; }

        [ForeignKey("userId")]
        public virtual ApplicationUser user { get; set; }
        [ForeignKey("groupId")]
        public virtual Group group { get; set; }
    }
}