using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Diagnostics;
using System.Dynamic;

namespace MongoDBtest.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.users_collection =
            Models.MongoHelper.database.GetCollection<Models.User>("User");
            var filter = Builders<Models.User>.Filter.Ne("_id", "");
            var result = Models.MongoHelper.users_collection.Find(filter).ToList();

            return View(result);
        }

        // GET: User/Details/5
        public ActionResult Details(string id)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.users_collection =
            Models.MongoHelper.database.GetCollection<Models.User>("User");
            var filter = Builders<Models.User>.Filter.Eq("_id", id);
            var result = Models.MongoHelper.users_collection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.users_collection =
                Models.MongoHelper.database.GetCollection<Models.User>("User");

                //Create some_id
                Object id = GenerateRandomId(24);

                Models.MongoHelper.users_collection.InsertOneAsync(new Models.User
                {

                    _id = id,
                    Username = collection["Username"],
                    Password = collection["Password"],
                    Firstname = collection["Firstname"],
                    Lastname = collection["Lastname"],
                    Emailaddress = collection["Email"],
                    Nickname = collection["Nickname"],
                    Gender = collection["Gender"]

                });
                return RedirectToAction("../Home/Index");
            }
            catch
            {
                return View();
            }
        }

        private static Random random = new Random();
        private object GenerateRandomId(int v)
        {
            string strarray = "abcdefghijklmnopqrstuvwxyz123456789";
            return new string(Enumerable.Repeat(strarray, v).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // GET: User/Edit/5
        public ActionResult Edit(string id)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.users_collection =
                Models.MongoHelper.database.GetCollection<Models.User>("User");
            var filter = Builders<Models.User>.Filter.Eq("_id", id);
            var result = Models.MongoHelper.users_collection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(String id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.users_collection =
                    Models.MongoHelper.database.GetCollection<Models.User>("User");
                var filter = Builders<Models.User>.Filter.Eq("_id", id);

                var update = Builders<Models.User>.Update
                    .Set("Username", collection["Username"])
                    .Set("Password", collection["Password"])
                    .Set("Firstname", collection["Firstname"])
                    .Set("Lastname", collection["Lastname"])
                    .Set("Emailaddress", collection["Emailaddress"])
                    .Set("Nickname", collection["Nickname"])
                    .Set("Gender", collection["Gender"]);
                var result = Models.MongoHelper.users_collection.UpdateOneAsync(filter, update);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(string id)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.users_collection =
                Models.MongoHelper.database.GetCollection<Models.User>("User");
            var filter = Builders<Models.User>.Filter.Eq("_id", id);
            var result = Models.MongoHelper.users_collection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.users_collection =
                Models.MongoHelper.database.GetCollection<Models.User>("User");
                var filter = Builders<Models.User>.Filter.Eq("_id", id);
                var result = Models.MongoHelper.users_collection.DeleteOneAsync(filter);
                ; return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //For The Post Page
        [HttpPost]
        public ActionResult CreateComment(FormCollection collection)
        {
            try
            {
                ViewBag.Username = Session["Username"];
                TempData["Username"] = Session["Username"];
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.Comments_collection =
                Models.MongoHelper.database.GetCollection<Models.Comments>("Comments");
                object id = GenerateRandomId(24);
                Models.MongoHelper.Comments_collection.InsertOneAsync(new Models.Comments
                {
                    _id = id,
                    Content = collection["comment"],
                    Post_id = Int16.Parse(collection["tex"]),
                });
                dynamic m = new ExpandoObject();
                m.posts = Models.MongoHelper.database.GetCollection<Models.Posts>("Posts");
                m.comments = Models.MongoHelper.database.GetCollection<Models.Comments>("Comments");
                //return View("../Home/Posts");
                return RedirectToAction("../Home/Posts");
            }
            catch
            {
                return View();
            }
        }
        // POST: Post/Create
        [HttpPost]
        public ActionResult CreatePost(FormCollection collection)
        {
            try
            {
                ViewBag.Username = Session["Username"];
                TempData["Username"] = Session["Username"];






                // TODO: Add insert logic here
                Models.MongoHelper.ConnectToMongoService();
                Models.MongoHelper.Posts_collection =
                Models.MongoHelper.database.GetCollection<Models.Posts>("Posts");

                int postid = Sequence.GetNextSequenceValue("postid", Models.MongoHelper.database);


                Models.MongoHelper.Posts_collection.InsertOneAsync(new Models.Posts
                {
                    _id = postid,
                    Postscontent = collection["Posts"],
                    Postsnikename = ViewBag.username,
                    Postscomment = collection["Postscomment"],
                    PostLikes = 0,                                    //  Nickname = collection["Nickname"],
                    ComComment = collection["ComComment"]


                });
                return RedirectToAction("../Home/Posts");
            }
            catch
            {
                return View();
            }


        }

        private static Random randoms = new Random();
        private object GenerateRandomIds(int vs)
        {
            string strarray = "abcdefghijklmnopqrstuvwxyz123456789";
            return new string(Enumerable.Repeat(strarray, vs).Select(s => s[randoms.Next(s.Length)]).ToArray());
        }

        public class Sequence
        {
            public string _id { get; set; }
            public int Seq { get; set; }

            public void Insert(IMongoDatabase database)
            {
                var collection = database.GetCollection<Sequence>("counters");
                collection.InsertOne(this);
            }

            internal static int GetNextSequenceValue(string sequenceName, IMongoDatabase database)
            {
                var collection = database.GetCollection<Sequence>("counters");
                var filter = Builders<Sequence>.Filter.Eq(a => a._id, sequenceName);
                var update = Builders<Sequence>.Update.Inc(a => a.Seq, 1);
                var sequence = collection.FindOneAndUpdate(filter, update);

                return sequence.Seq;
            }
        }
    }
}
