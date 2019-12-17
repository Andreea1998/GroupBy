﻿using Facebook.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
			IEnumerable<Album> albums = db.Albums.Where(m => m.userId == id).ToList();
			//Might not need 
			ViewBag.Albums = albums;
			ViewBag.userId = id;
			//
			return View(albums);
		}

		public ActionResult New(string id)
		{
			ApplicationUser user = db.Users.Find(id);
			Album album = new Album();
			album.user = user;
			album.userId = id;
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
					return RedirectToAction("Index", "Album", new { @id = album.userId });
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

		[Authorize(Roles = "Editor,Administrator")]
		public ActionResult Edit(string id)
		{
			Album album = db.Albums.Find(id);
			ViewBag.Album = album;
			ApplicationUser user = db.Users.Find(album.user.Id);

			if (album.userId == User.Identity.GetUserId() ||
				User.IsInRole("Administrator"))
			{
				return View(album);
			}
			else
			{
				TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui album care nu va apartine!";
				return RedirectToAction("Index");
			}

		}

		[HttpPut]
		//[Authorize(Roles = "Editor,Administrator")]
		public ActionResult Edit(string id, Album requestAlbum)
		{

			try
			{
				if (ModelState.IsValid)
				{
					Album album = db.Albums.Find(id);
					if (album.userId == User.Identity.GetUserId() ||
						User.IsInRole("Administrator"))
					{
						if (TryUpdateModel(album))
						{
							album.name = requestAlbum.name;
							db.SaveChanges();
							TempData["message"] = "Albumul a fost modificat!";
						}
						return RedirectToAction("Index");
					}
					else
					{
						TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui album care nu va apartine!";
						return RedirectToAction("Index");
					}

				}
				else
				{
					return View(requestAlbum);
				}

			}
			catch (Exception e)
			{
				return View(requestAlbum);
			}
		}

		[HttpDelete]
		//[Authorize(Roles = "Editor,Administrator")]
		public ActionResult Delete(string id)
		{
			Album album = db.Albums.Find(id);
			if (album.userId == User.Identity.GetUserId() ||
				User.IsInRole("Administrator"))
			{
				db.Albums.Remove(album);
				db.SaveChanges();
				TempData["message"] = "Albumul a fost sters!";
				return RedirectToAction("Index");
			}
			else
			{
				TempData["message"] = "Nu aveti dreptul sa stergeti un album care nu va apartine!";
				return RedirectToAction("Index");
			}

		}

	}
}