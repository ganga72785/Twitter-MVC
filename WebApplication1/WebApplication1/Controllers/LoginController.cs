using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.ExceptionFilters;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        //public static List<User> objUserlist = new List<Models.User>()
        //{
        //    new Models.User(){User_id=1,Username="Admin",Password="Admin",Active=true,Fullname="Admin",Email="Admin@test.com"},
        //    new Models.User(){User_id=2,Username="Test",Password="Test",Active=true,Fullname="Test",Email="Test@test.com"},
        //     new Models.User(){User_id=3,Username="Test12",Password="Test12",Active=true,Fullname="Test12",Email="Test12@test.com"},
        //};

     
        // GET: MyTwitterClone
        [HttpGet]
       // [OutputCache(Duration =30,Location =System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Login()
        {
            Session["username"] = null;
            using (TwitterContext db = new TwitterContext())
            {
                if (db.Users.Count() == 0)
                {
                    User p = new User() { Username = "Admin", Fullname = "Admin", Password = "Admin", Active = true, Email = "Admin@adm.com", Joined = DateTime.Today.AddDays(-100), User_id = 1 };
                    db.Users.Add(p);
                    p = new User() { Username = "Ganga", Fullname = "Gangadharan", Password = "Ganga", Active = true, Email = "Ganga@adm.com", Joined = DateTime.Today.AddDays(-100), User_id = 2 };
                    db.Users.Add(p);
                    p = new User() { Username = "Test", Fullname = "Test", Password = "Test", Active = true, Email = "Test@adm.com", Joined = DateTime.Today.AddDays(-100), User_id = 3 };
                    db.Users.Add(p);
                }


                if (db.TweetList.Count() == 0)
                {
                    Tweets d = new Tweets() { User_id = 1, Created = DateTime.Now.AddDays(-15), Message = "Hi All Welcome..", Tweet_id = 1 };
                    db.TweetList.Add(d);
                    d = new Tweets() { User_id = 2, Message = "Test message", Created = DateTime.Now.AddDays(-4) };
                    db.TweetList.Add(d);
                    d = new Tweets() { User_id = 3, Message = "Test message2", Created = DateTime.Now.AddDays(-5) };
                    db.TweetList.Add(d);
                }

                if (db.FollowingList.Count() == 0)
                {
                    Following F = new Following() { User_id = 1, Following_id = 1 };
                    db.FollowingList.Add(F);
                    F = new Following() { User_id = 2, Following_id = 1 };
                    db.FollowingList.Add(F);
                    F = new Following() { User_id = 3, Following_id = 1 };
                    db.FollowingList.Add(F);
                }
                db.SaveChanges();
            }
            //ViewBag["ErrorMessage"] = string.Empty;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string uname, string pwd)
        {
            using (TwitterContext db = new TwitterContext())
            {
                var user = db.Users.ToList().FirstOrDefault(e => e.Username == uname && e.Password == pwd);
                if (user != null)
                {
                    //TempData["Uname"] = user.Username;
                    return RedirectToAction("Index", "Home", new { username = uname });
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid User";
                    return View();
                }
                //return View();
            }
                     
        }

        [OutputCache(Duration = 30, Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Signup()
        {
            return RedirectToAction("Signup","Home");
        }

        [OutputCache(Duration = 30, Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Home()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}