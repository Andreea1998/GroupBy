using Facebook.Models;
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

		// GET: Photo
		[Route("Index/{id}")]
		public ActionResult Index(int id)
		{
			IEnumerable<Photo> photos = db.Photos.Where(m => m.albumID == id).ToList();
			Album album = db.Albums.Find(id);
			ViewBag.albumName = album.name;
			ViewBag.albumId = id;
			return View(photos);
		}

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
	}
}