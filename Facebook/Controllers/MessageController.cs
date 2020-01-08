using Facebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Facebook.Controllers
{
    public class MessageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Message
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User,Administrator")]
        public ActionResult New(Message message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = db.Users.Find(message.userID);
                    Profile profile = db.Profiles.Where(data => data.userId == user.Id).ToList()[0];
                    message.userName = profile.name;
                    db.Messages.Add(message);
                    db.SaveChanges();
                    return RedirectToAction("Show", "Group", new { id = message.groupID });
                }
                else
                {
                    return RedirectToAction("Show", "Group", new { id = message.groupID });
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Show", "Group", new { id = message.groupID });
            }
        }
    }
}