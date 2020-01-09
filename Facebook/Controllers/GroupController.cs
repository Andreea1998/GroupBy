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
    public class GroupController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

		// GET: Group
		[Authorize(Roles = "User,Administrator")]
		public ActionResult Index(string id)
        {
            IEnumerable<UserGroup> userGroups = db.UserGroups.Where(m => m.userId == id).ToList();
            List<Group> groups = new List<Group>();
            foreach(UserGroup userGroup in userGroups)
            {
                Group group = db.Groups.Find(userGroup.groupId);
                groups.Add(group);
            }
            ViewBag.groupsOwner = id;
           
            return View(groups);
        }

		[Authorize(Roles = "User,Administrator")]
		public ActionResult Show(int id)
        {
            IEnumerable<UserGroup> userGroups = db.UserGroups.Where(m => m.groupId == id).ToList();
            List<Profile> profiles = new List<Profile>();
            foreach(UserGroup userGroup in userGroups)
            {
                Profile profile = db.Profiles.Where(m => m.userId == userGroup.userId).ToList()[0];
                profiles.Add(profile);

            }
            ICollection<Message> messages = db.Messages.Where(m => m.groupID == id).ToList();
            Group group = db.Groups.Find(id);
            GroupChat groupChat = new GroupChat();
            groupChat.users = profiles;
            groupChat.messages = messages;
            groupChat.group = group;

            ViewBag.currentUser = User.Identity.GetUserId();
            return View(groupChat);
        }

		[Authorize(Roles = "User,Administrator")]
		public ActionResult New(string id)
        {
            Group group = new Group();
            group.creatorId = id;
            return View(group);
        }

        [HttpPost]
        [Authorize(Roles = "User,Administrator")]
        public ActionResult New(Group group)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Groups.Add(group);
                    UserGroup userGroup = new UserGroup();
                    userGroup.userId = group.creatorId;
                    userGroup.groupId = group.groupID;
                    db.UserGroups.Add(userGroup);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Group", new { id = group.creatorId });
                }
                else
                {
                    return View(group);
                }
            }
            catch (Exception e)
            {
                return View(group);
            }
        }

        [Authorize(Roles ="User,Administrator")]
        public ActionResult AddUserIndex(int id)
        {
            string userId = User.Identity.GetUserId();
            IEnumerable<Friends> requests = db.Friends.Where((m => (m.requestBy == userId || m.requestTo == userId) && (m.friends == true))).ToList();
            List<Profile> friends = new List<Profile>();

            foreach (Friends request in requests)
            {
                Profile profile;
                if (userId == request.requestBy)
                {
                    profile = db.Profiles.Where(m => m.userId == request.requestTo).ToList()[0];
                }
                else
                {
                    profile = db.Profiles.Where(m => m.userId == request.requestBy).ToList()[0];
                }
                friends.Add(profile);
            }
            ViewBag.groupId = id;
            return View(friends);
        }

        [Authorize(Roles = "User,Administrator")]
        public ActionResult AddToGroup(int groupId,string userId)
        {
            UserGroup userGroup = new UserGroup();
            userGroup.groupId = groupId;
            userGroup.userId = userId;
            db.UserGroups.Add(userGroup);
            db.SaveChanges();
            return RedirectToAction("Show", "Group", new { @id = groupId });
        }
    }
}