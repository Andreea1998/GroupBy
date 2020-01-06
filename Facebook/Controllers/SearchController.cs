using Facebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Facebook.Controllers
{
    public class SearchController : Controller
    {
		private ApplicationDbContext db = new ApplicationDbContext();
		public ActionResult Search(string searchText)
        {
			List<Search> results = new List<Search>();
			IEnumerable<Profile> profiles = db.Profiles.Where(profile => profile.name.ToLower().Contains(searchText.ToLower())).ToList();
			foreach(Profile profile in profiles)
			{
				ApplicationUser user = db.Users.Find(profile.userId);
				Search searchResult = new Search();
				searchResult.user = user;
				searchResult.profile = profile;
				results.Add(searchResult);
			}
            return View(results);
        }
    }
}