using Facebook.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Facebook.Controllers
{
    public class CommentController : Controller
    {

		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: Comment
		public ActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public ActionResult New(Comment comment)
		{
			try
			{
				if (ModelState.IsValid)
				{
					ApplicationUser user = db.Users.Find(comment.userID);
					comment.userName = user.UserName;
					db.Comments.Add(comment);
					db.SaveChanges();
					TempData["message"] = "Comentariul a fost creat!";
					return RedirectToAction("Show", "Photo", new { id = comment.photoID });
				}
				else
				{
					Debug.WriteLine("Hello?");
					return RedirectToAction("Show", "Photo", new { id = comment.photoID });
				}
			}
			catch (Exception e)
			{
				return RedirectToAction("Show", "Photo", new { id = comment.photoID });
			}
		}
    }
}