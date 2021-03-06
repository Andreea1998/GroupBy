﻿using Facebook.Models;
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
		[Authorize(Roles = "User,Administrator")]
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
					return RedirectToAction("Show", "Photo", new { id = comment.photoID });
				}
			}
			catch (Exception e)
			{
				return RedirectToAction("Show", "Photo", new { id = comment.photoID });
			}
		}

        [HttpDelete]
		[Authorize(Roles = "User,Administrator")]
		public ActionResult Delete(int commentId,int photoId)
        {
			
            Comment comment = db.Comments.Find(commentId);
            db.Comments.Remove(comment);
            db.SaveChanges();
			return RedirectToAction("Show", "Photo", new { id = photoId});
		}

		[Authorize(Roles = "User,Administrator")]
		public ActionResult Edit(int id)
		{
			Comment comment = db.Comments.Find(id);
			return View(comment);
		}

		[HttpPut]
		[Authorize(Roles = "User,Administrator")]
		public ActionResult Edit(Comment reqComment)
		{
			try
			{
				if(ModelState.IsValid)
				{
					Comment comment = db.Comments.Find(reqComment.commentID);
					if(TryUpdateModel(comment))
					{
						comment.text = reqComment.text;
						db.SaveChanges();
					}
					return RedirectToAction("Show", "Photo", new { @id = comment.photoID });
				}
				else
				{
					return View(reqComment);
				}
			}
			catch(Exception e)
			{
				return View(reqComment);
			}
		}
	}
}