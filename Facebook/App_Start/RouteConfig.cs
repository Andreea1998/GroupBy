using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Facebook
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			
			routes.MapRoute(
				"DeleteComment",                                              // Route name
				"Comment/Delete/{commentId}/{photoId}",                           // URL with parameters
				new { controller = "Comment", action = "Delete", commentId=UrlParameter.Optional, photoId = UrlParameter.Optional }  // Parameter defaults
			);
			routes.MapRoute(
				"Search",                                              
				"Search/Search/{searchText}",                          
				new { controller = "Search", action = "Search", searchText = UrlParameter.Optional } 
			);
            routes.MapRoute(
                "FriendRequest",
                "Friends/SendFriendRequest/{requestBy}/{requestTo}",
                new { controller = "Friends", action = "SendFriendRequest", requestBy = UrlParameter.Optional , requestTo = UrlParameter.Optional }
            );

            routes.MapRoute(
                "AcceptFriendRequest",
                "Friends/AcceptFriendRequest/{requestBy}/{requestTo}",
                new { controller = "Friends", action = "AcceptFriendRequest", requestBy = UrlParameter.Optional, requestTo = UrlParameter.Optional }
            );
            routes.MapRoute(
                "DeleteFriendRequest",
                "Friends/DeleteFriendRequest/{requestBy}/{requestTo}",
                new { controller = "Friends", action = "DeleteFriendRequest", requestBy = UrlParameter.Optional, requestTo = UrlParameter.Optional }
            );
            routes.MapRoute(
                "AddUserToGroup",
                "Group/AddToGroup/{groupId}/{userId}",
                new { controller = "Group", action = "AddToGroup", groupId = UrlParameter.Optional, userId = UrlParameter.Optional }
            );



            routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
