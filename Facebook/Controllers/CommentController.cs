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
                    Profile profile = db.Profiles.Where(data => data.userId == user.Id).ToList()[0];
                    comment.userName = profile.name;
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

        [HttpDelete]
        public ActionResult Delete(int commentId,int photoId)
        {
			
            Comment comment = db.Comments.Find(commentId);
            db.Comments.Remove(comment);
            db.SaveChanges();
			return RedirectToAction("Show", "Photo", new { id = photoId});
		}
    }
}