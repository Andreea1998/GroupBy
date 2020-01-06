using Facebook.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Facebook.Controllers
{   
	[Authorize]
    public class ProfileController : Controller
    {
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: Profile
		//Sters?
		public ActionResult Index()
        {
            return View();
        }

		[Route("Show/{id}")]
		public ActionResult Show(string id)
		{
			//System.Diagnostics.Debug.WriteLine(id);
			ApplicationUser user = db.Users.Find(id);
			//System.Diagnostics.Debug.WriteLine(user.profileID);
			Profile profile = db.Profiles.Find(user.profileID);

			ViewBag.LoggedIn = false;
			ViewBag.Administrator = false;
            ViewBag.currentUserId = User.Identity.GetUserId();
            Debug.WriteLine(user.Id);

			if (User.IsInRole("User"))
			{
				ViewBag.LoggedIn = true;
			}
			if (User.IsInRole("Administrator"))
			{
				ViewBag.Administrator = true;
			}

            ViewBag.LoggedIn = true;

            return View(profile);
		}

		public ActionResult New()
		{
			Profile profile = new Profile();
			//ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
			profile.userId = User.Identity.GetUserId(); 
			return View(profile);
		}

		[HttpPost]
		//[Authorize(Roles = "User,Administrator")]
		public ActionResult New(Profile profile)
		{
			try
			{
				if(ModelState.IsValid)
				{
					db.Profiles.Add(profile);
					db.SaveChanges();
					ApplicationUser user = db.Users.Find(profile.userId);
					TempData["message"] = "Profilul a fost creat!";
					if (TryUpdateModel(user))
					{
						user.profileID = profile.profileID;
						db.SaveChanges();
					}
					return RedirectToAction("Show","Profile",new { id = profile.userId });
				}
				else
				{
					System.Diagnostics.Debug.WriteLine("This will be displayed in output window");
					return View(profile);
				}
			}
			catch(DbEntityValidationException dbEx)
			{
				foreach (var validationErrors in dbEx.EntityValidationErrors)
				{
					foreach (var validationError in validationErrors.ValidationErrors)
					{
						Trace.TraceInformation(
							  "Class: {0}, Property: {1}, Error: {2}",
							  validationErrors.Entry.Entity.GetType().FullName,
							  validationError.PropertyName,
							  validationError.ErrorMessage);
					}
				}
				return View(profile);
			} 
		}

        //[Authorize(Roles = "Editor,Administrator")]
        public ActionResult Edit(int id)
        {
            Profile profile = db.Profiles.Find(id);

            if (profile.userId == User.Identity.GetUserId() ||
                User.IsInRole("Administrator"))
            {
                return View(profile);
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui Profile care nu va apartine!";
                return RedirectToAction("Show",new {id=profile.userId });
            }

        }

        [HttpPut]
        public ActionResult Edit(Profile requestProfile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Profile profile = db.Profiles.Find(requestProfile.profileID);
                    if (TryUpdateModel(profile))
                    {
                        profile.profileImageUrl = requestProfile.profileImageUrl;
                        profile.backgroundImageUrl = requestProfile.backgroundImageUrl;
                        profile.name = requestProfile.name;
                        profile.about = requestProfile.about;
                        profile.occupation = requestProfile.occupation;
                        profile.studies = requestProfile.studies;
                        profile.languages = requestProfile.languages;
                        profile.job = requestProfile.job;
                        profile.address = requestProfile.address;
                        profile.phone = requestProfile.phone;
                        profile.email = requestProfile.email;
                        profile.web = requestProfile.web;

                        //profile.privateP = requestProfile.privateP;
                        db.SaveChanges();
                        TempData["message"] = "Profilul a fost modificat!";
                    }

                    return RedirectToAction("Show", new { id = profile.userId });
                }
                else
                {
                    return View(requestProfile);
                }
            }
            catch (Exception e)
            {
                
                return View(requestProfile);
            }
        }

    }
}