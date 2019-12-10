using Facebook.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Facebook.Controllers
{
	public class AlbumController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: Album
		[Route("Index/{id}")]
		public ActionResult Index(string id)
		{
			ApplicationUser user = db.Users.Find(id);
			//NEEDS WORK
			IEnumerable<Album> albums = db.Albums.Where(m => m.user.Id == id).ToList();
			ViewBag.Albums = albums;
			ViewBag.userId = id;
			return View(albums);
		}

		public ActionResult New(string id)
		{
			ApplicationUser user = db.Users.Find(id);
			Album album = new Album();
			album.user = user;
			return View(album);
		}

		[HttpPost]
		public ActionResult New(Album album)
		{
			try
			{
				if (ModelState.IsValid)
				{
					db.Albums.Add(album);
					db.SaveChanges();
					ApplicationUser user = db.Users.Find(album.user.Id);
					TempData["message"] = "Albumul a fost creat!";
					if (TryUpdateModel(user))
					{
						user.albums.Add(album);
						db.SaveChanges();
					}
					return RedirectToAction("Index", "Album", new { id = album.user.Id });
				}
				else
				{
					System.Diagnostics.Debug.WriteLine("This will be displayed in output window");
					return View(album);
				}
			}
			catch(Exception e)
			{
				return View(album);
			}
		}
    }
}