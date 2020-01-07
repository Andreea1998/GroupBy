using Facebook.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Facebook.Controllers
{
    public class FriendsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowFriends(string id)
        {
            IEnumerable<Friends> requests = db.Friends.Where((m => (m.requestBy == id || m.requestTo == id) && (m.friends == true))).ToList();
            List<Profile> friends = new List<Profile>();
            
            foreach(Friends request in requests)
            {
                Profile profile;
                if(id==request.requestBy)
                {
                    profile = db.Profiles.Where(m => m.userId == request.requestTo).ToList()[0];
                }
                else
                {
                    profile = db.Profiles.Where(m => m.userId == request.requestBy).ToList()[0];
                }
                friends.Add(profile);
            }

            ViewBag.loggedIn = false;
            ViewBag.ownsProfile = id;
            if(User.IsInRole("User")||User.IsInRole("Administrator"))
            {
                ViewBag.loggedInUser = User.Identity.GetUserId();
                ViewBag.loggedIn = true;
            }

            return View(friends);
        }

        public ActionResult ShowFriendRequests(string id)
        {
            IEnumerable<Friends> requests = db.Friends.Where(m => m.requestTo==id && m.friends == false).ToList();
            List<Profile> friends = new List<Profile>();

            foreach (Friends request in requests)
            {
                Profile profile;
                if (id == request.requestBy)
                {
                    profile = db.Profiles.Where(m => m.userId == request.requestTo).ToList()[0];
                }
                else
                {
                    profile = db.Profiles.Where(m => m.userId == request.requestBy).ToList()[0];
                }
                friends.Add(profile);
            }

            ViewBag.loggedInUser = User.Identity.GetUserId();
            return View(friends);
        }

        [HttpPost]
        public ActionResult SendFriendRequest(string requestBy,string requestTo)
        {
            Friends request = new Friends();
            request.requestBy = requestBy;
            request.requestTo = requestTo;
            request.friends = false;
            db.Friends.Add(request);
            db.SaveChanges();
            return RedirectToAction("Show", "Profile", new { @id = requestTo });
        }

        public ActionResult AcceptFriendRequest(string requestBy, string requestTo)
        {
            Friends request = db.Friends.Where(m => m.requestBy == requestBy && m.requestTo == requestTo).ToList()[0];
            if(TryUpdateModel(request))
            {
                request.friends = true;
                db.SaveChanges();
            }

            return RedirectToAction("ShowFriendRequests", "Friends", new { @id = requestTo });
        }

        public ActionResult DeleteFriendRequest(string requestBy, string requestTo)
        {
            Friends request = db.Friends.Where(m => (m.requestBy == requestBy && m.requestTo == requestTo)|| (m.requestBy == requestTo && m.requestTo == requestBy)).ToList()[0];
            db.Friends.Remove(request);
            db.SaveChanges();
            return RedirectToAction("ShowFriends", "Friends", new { @id = requestTo });
        }

    }
}