using Facebook.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Facebook.Controllers
{
    public class PhotoController : Controller
    {
		private ApplicationDbContext db = new ApplicationDbContext();

		//Get all photo with id as albumId
		[Route("Index/{id}")]
		public ActionResult Index(int id)
		{
			IEnumerable<Photo> photos = db.Photos.Where(m => m.albumID == id).ToList();
			Album album = db.Albums.Find(id);
			ViewBag.albumName = album.name;
			ViewBag.albumId = id;
			return View(photos);
		}

		//Create new photo with id as albumId
		public ActionResult New(int id)
		{
			Album album = db.Albums.Find(id);
			Photo photo = new Photo();
			photo.album = album;
			photo.albumID = id;
			return View(photo);
		}

		[HttpPost]
		public ActionResult New(Photo photo)
		{
			try
			{
				if (ModelState.IsValid)
				{
					db.Photos.Add(photo);
					db.SaveChanges();
					TempData["message"] = "Poza a fost creata!";
					return RedirectToAction("Index", "Photo", new { id = photo.albumID });
				}
				else
				{
					return View(photo);
				}
			}
			catch (Exception e)
			{
				return View(photo);
			}
		}

		
		[HttpGet]
		public ActionResult Show(int id)
		{
			Photo photo = db.Photos.Find(id);

            Album album = db.Albums.Find(photo.albumID);
            ApplicationUser user = db.Users.Find(album.userId);
            Profile profile = db.Profiles.Where(data => data.userId == user.Id).ToList()[0];
            IEnumerable<Comment> comments = db.Comments.Where(m => m.photoID == id).ToList();
			PhotoWithComments photoWithComments = new PhotoWithComments();
			photoWithComments.photo = photo;
			photoWithComments.comments = comments;

			ViewBag.currentUser = User.Identity.GetUserId();
            //Info about the user who owns the photo
            ViewBag.userName = profile.name;
            ViewBag.userId = user.Id;

			return View(photoWithComments);
		}
	}
}