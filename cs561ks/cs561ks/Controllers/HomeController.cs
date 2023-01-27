using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using MongoDBtest.Models;
using System.Dynamic;
using System.Diagnostics;

namespace MongoDBtest.Controllers
{
    public class HomeController : Controller
    {
        public static String usernameg;
        public bool logincheck(string username, string password)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.users_collection =
            Models.MongoHelper.database.GetCollection<Models.User>("User");

            var filterbyun = Builders<Models.User>.Filter.Eq("Username", username);
            var filterbypw = Builders<Models.User>.Filter.Eq("Password", password);
            var filter = filterbyun & filterbypw;

            var result = Models.MongoHelper.users_collection.Find(filter).ToList().FirstOrDefault();

            if (result != null)
                return true;
            else
                return false;
        }
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Likebut(FormCollection collection)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.Posts_collection =
            Models.MongoHelper.database.GetCollection<Models.Posts>("Posts");
            var filter = Builders<Models.Posts>.Filter.Eq("_id", collection["pid"]);
            Debug.WriteLine(collection["pid"]);
            var result = Models.MongoHelper.Posts_collection.Find(filter).ToList();
            var update = Builders<Models.Posts>.Update.Inc(a => a.PostLikes, 1);
            Models.MongoHelper.Posts_collection.FindOneAndUpdate(filter, update);
            //Do not change or remove the two lines above. They are for toggling the Like button guand the Unlike button
            return RedirectToAction("Posts");
        }


        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            //ViewBag.col = collection["username"];
            //bool res = MongoHelper.logincheck(collection["username"], collection["password"]);
            /*Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.users_collection =
            Models.MongoHelper.database.GetCollection<Models.User>("User");

            var filterbyun = Builders<Models.User>.Filter.Eq("Username", collection["username"]);
            var filterbypw = Builders<Models.User>.Filter.Eq("Password", collection["password"]);
            var filter = filterbyun & filterbypw;

            //ViewBag.fil = filterbyun.ToList();
            //return View();
            var result = Models.MongoHelper.users_collection.Find(filter).ToList().FirstOrDefault();
            */

            if (logincheck(collection["Username"], collection["Password"]) == true)
            {
                string Username = collection["Username"];
                Session["Username"] = Username;
                usernameg = Username;
                return RedirectToAction("../Home/Posts");
            }
            else
                return RedirectToAction("Index");
            /*if (result != null)
            {
                return RedirectToAction("../User/Index");
            }
            else
            {
                return RedirectToAction("Index");
            }*/
        }

        public ActionResult Register()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Posts()
        {
            if (Session["Username"] != null)
            {
                ViewBag.Username = Session["Username"];
                TempData["Username"] = Session["Username"];
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.Posts_collection =
                Models.MongoHelper.database.GetCollection<Models.Posts>("Posts");
                var filter = Builders<Models.Posts>.Filter.Ne("Postcontent", "");
                var result1 = Models.MongoHelper.Posts_collection.Find(filter).ToList();
                Models.MongoHelper.Comments_collection = Models.MongoHelper.database.GetCollection<Models.Comments>("Comments");
                var filter2 = Builders<Models.Comments>.Filter.Ne("_id", "");
                var result2 = Models.MongoHelper.Comments_collection.Find(filter2).ToList();
                dynamic mymodel = new ExpandoObject();
                mymodel.Posts = result1;
                mymodel.Comments = result2;
                return View(mymodel);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
        public ActionResult ShowPosts(string un)
        {
            if (un != null)
            {
                ViewBag.Username = un;
                TempData["Username"] = un;
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.Posts_collection =
                Models.MongoHelper.database.GetCollection<Models.Posts>("Posts");
                var filter = Builders<Models.Posts>.Filter.Ne("Username", "");
                var result = Models.MongoHelper.Posts_collection.Find(filter).ToList();
                return View(result);
            }
            else
            {
                return null;
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("/Index");
        }

        public new ActionResult Profile()
        {
            if (Session["Username"] != null)
            {
                ViewBag.Message = "Profile";
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.users_collection =
                Models.MongoHelper.database.GetCollection<Models.User>("User");
                var result = Builders<Models.User>.Filter.Eq("Username", usernameg);
                //var result = Models.MongoHelper.users_collection.Find(r => r.Username == "sithulin");
                var result1 = Models.MongoHelper.users_collection.Find(result).ToList();
                TempData.Keep();
                return View(result1);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult showProfile(string un)
        {
            if (un != null)
            {
                ViewBag.Username = un;
                TempData["Username"] = un;
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}