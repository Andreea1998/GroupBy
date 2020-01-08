using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
    public class GroupChat
    {
        public Group group { get; set; }
        public ICollection<Profile> users { get; set; }
        public ICollection<Message> messages { get; set; }
    }
}