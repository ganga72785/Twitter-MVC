using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {


        //public static List<Tweets> objTweetlist = new List<Models.Tweets>()
        //{
        //    new Models.Tweets(){User_id=1,Message="Admin message",Created=DateTime.Now.AddDays(-10)},
        //    new Models.Tweets(){User_id=2,Message="Test message",Created=DateTime.Now.AddDays(-4)},
        //    new Models.Tweets(){User_id=3,Message="Test message2",Created=DateTime.Now.AddDays(-5)},
        //};

        //public static List<Following> objFollowinglist = new List<Models.Following>()
        //{
        //    new Models.Following(){User_id=1,Following_id=1},
        //    new Models.Following(){User_id=1,Following_id=3},
        //};

        public List<ViewModel> GetAllTweets()
        {
            string strusername = Session["username"].ToString();
            using (TwitterContext db = new TwitterContext())
            {
            List<ViewModel> lstviewmodel = new List<ViewModel>();
                ViewModel objViewModel;
                List<User> objUser = db.Users.ToList<User>();
                foreach (var item in db.TweetList)
                {
                    objViewModel = new ViewModel();
                    objViewModel.Tweet_id = item.Tweet_id;
                    objViewModel.Username = objUser.Where(e => e.User_id == item.User_id).FirstOrDefault().Username;
                    objViewModel.Message = item.Message;
                    objViewModel.Created = item.Created;
                    lstviewmodel.Add(objViewModel);
                    
                }
                lstviewmodel = lstviewmodel.OrderByDescending(s => s.Created).ToList();                
                ViewBag.Following = db.FollowingList.Where(e => e.User_id == db.Users.Where(x => x.Username == strusername).FirstOrDefault().User_id).Count();
                ViewBag.Follower = "3";
                ViewBag.Tweets = db.TweetList.Where(e => e.User_id == db.Users.Where(t => t.Username == strusername).FirstOrDefault().User_id).Count();
                ViewBag.Username = strusername;
                return lstviewmodel;
            }
                
        }

        public ActionResult Index(string username)
        {
            Session["username"] = username;
            return View(GetAllTweets());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your are viewing Partial View.";

            return PartialView("_Partial");
        }

        public ActionResult Update(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                using (TwitterContext db = new TwitterContext())
                {
                    List<User> objUserlist = new List<Models.User>();
                    objUserlist.AddRange(db.Users);
                    db.TweetList.Add(new Tweets { Message = message, User_id = objUserlist.Where(e => e.Username == Session["username"].ToString()).FirstOrDefault().User_id, Created = DateTime.Now });
                    db.SaveChanges();
                    return View("Index", GetAllTweets());
                }
            }
            else
            {
                ViewBag.Errormessage = "Tweet message cannot be empty";
                return View("Index", GetAllTweets());
            }
        }
        

        public ActionResult Search(string strSearch)
        {
            using (TwitterContext db = new TwitterContext())
            {
                FollowingViewModel objfollowingViewModel;
                List<User> objUserList = new List<Models.User>();
                objUserList.AddRange(db.Users);
                var onbj = objUserList.Where(e => e.Username.ToUpper().Equals(strSearch.ToUpper())).FirstOrDefault();
                if (onbj != null)
                {
                    objfollowingViewModel = new FollowingViewModel();
                    objfollowingViewModel.Username = onbj.Username;
                    objfollowingViewModel.Current_user_id = objUserList.Where(e => e.Username == Session["username"].ToString()).FirstOrDefault().User_id;
                    return RedirectToAction("Search", "Search", objfollowingViewModel);
                }
                return View();
            }
        }
        
        [HttpGet]
        public ActionResult Signup()
        {
            using (TwitterContext db = new TwitterContext())
            {
                ViewBag.ErrorMessage = string.Empty;
                //ViewBag.Message = "Your application description page.";
                string unamedis = Session["username"] != null ? Session["username"].ToString() : null;
                if (unamedis != null)
                    return View(db.Users.Where(e => e.Username == unamedis).FirstOrDefault());
                else
                {
                    User objusr = new Models.User();
                    return View(objusr);
                }
            }
        }

        [HttpPost]
        public ActionResult Signup(User objUser)
        {
            using (TwitterContext db = new TwitterContext())
            {
                if (objUser != null)
                {
                    List<User> objUserList = new List<Models.User>();
                    objUserList.AddRange(db.Users);
                    if (objUserList.Where(e => e.Username == objUser.Username).Count() > 0)
                    {   
                        var Updateuser = objUserList.Where(e => e.Username == objUser.Username).FirstOrDefault();
                        Updateuser.Fullname = objUser.Fullname;
                        Updateuser.Password = objUser.Password;                        
                        db.SaveChanges();
                        return View(Updateuser);
                    }
                    else
                    {
                        db.Users.Add(objUser);
                        db.SaveChanges();
                        return RedirectToAction("Login", "Login");
                    }
                }
                return View();
            }
        }

        public ActionResult Login()
        {
            return RedirectToAction("Login","Login");
        }
    }
}