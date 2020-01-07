using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
    public class Friends
    {
        //Id of the user who initiated the friend request
        [Key,Column(Order =0)]
        public string requestBy { get; set; }
        //Id of the user who recieved the friend request
        [Key,Column(Order =1)]
        public string requestTo { get; set; }

        [DefaultValue("false")]
        public bool friends { get; set; }

        [ForeignKey("requestBy")]
        public virtual ApplicationUser requestByUser { get; set; }
        [ForeignKey("requestTo")]
        public virtual ApplicationUser requestToUser { get; set; }
    }
}