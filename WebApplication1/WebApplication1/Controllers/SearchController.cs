using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Search(FollowingViewModel objfollowingViewModel)
        {
            return View(objfollowingViewModel);
        }

        [HttpPost]
        public ActionResult Submit(FollowingViewModel objfollowingViewModel)
        {
            using (TwitterContext db = new TwitterContext())
            {
                List<User> objUsersList = new List<Models.User>();
                objUsersList.AddRange(db.Users);
                Following objFollowing = new Following();
                objFollowing.Following_id = objUsersList.Where(e => e.Username == objfollowingViewModel.Username).FirstOrDefault().User_id;
                objFollowing.User_id = objfollowingViewModel.Current_user_id;
                db.FollowingList.Add(objFollowing);
                db.SaveChanges();
                //Session["username"]= objUsersList.Where(e => e.User_id == objfollowingViewModel.Current_user_id).FirstOrDefault().Username.ToString();
                return RedirectToAction("Index", "Home", new { username = objUsersList.Where(e => e.User_id == objfollowingViewModel.Current_user_id).FirstOrDefault().Username.ToString() });
            }
             
        }
    }
}