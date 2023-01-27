using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;

namespace MongoDBtest.Controllers
{
    public class PostsController : Controller
    {/*
       //  GET: Posts
         public ActionResult Index()
         {
             Models.MongoHelper.ConnectToMongoService();
             Models.MongoHelper.Posts_collection =
             Models.MongoHelper.database.GetCollection<Models.Posts>("User");
             var filter = Builders<Models.Posts>.Filter.Ne("PostID",PostID);
             var result = Models.MongoHelper.Posts_collection.Find(filter).ToList();

             return View(result);
         }

         GET: Posts/Details/5
          public ActionResult Details(string id)
          {
              Models.MongoHelper.ConnectToMongoService();
              Models.MongoHelper.Posts_collection =
              Models.MongoHelper.database.GetCollection<Models.Posts>("Postnickname");
              var filter = Builders<Models.User>.Filter.Eq(PostId);
              var result = Models.MongoHelper.Posts_collection.Find(filter).FirstOrDefault();
              return View(result);
          }

         //GET: Post/Create
        public ActionResult Create()
{

    return View();
}

            //POST: Post/Create
               //[HttpPost]
        public ActionResult Create(FormCollection collection)
{
    try
    {
   // TODO: Add insert logic here
                Models.MongoHelper.ConnectToMongoService();
        Models.MongoHelper.Posts_collection =
        Models.MongoHelper.database.GetCollection<Models.Posts>("Posts");



        Models.MongoHelper.Posts_collection.InsertOneAsync(new Models.Posts
        {


            Postscontent = collection["Postscontent"],
            Postsnikename = collection["Postsnikename"],
            Postscomment = collection["Postscomment"],
            Postlikes = collection["PostLikes"],
            Nickname = collection["Nickname"],
            ComComment = collection["ComComment"]

        });
        return RedirectToAction("../Home/Posts");
    }
    catch
    {
        return View();
    }
}


                //GET: Posts/Edit/5
          public ActionResult Edit(string id)
        {
             Models.MongoHelper.ConnectToMongoService();
             Models.MongoHelper.Posts_collection =
                 Models.MongoHelper.database.GetCollection<Models.Posts>("Posts");
             var filter = Builders<Models.User>.Filter.Eq("_id", id);
             var result = Models.MongoHelper.Posts_collection.Find(filter).FirstOrDefault();
             return View(result);
         }

         //POST: Posts/Edit/5
        [HttpPost]
public ActionResult Edit(String id, FormCollection collection)
          {
              try
              {
                   TODO: Add update logic here
                  Models.MongoHelper.ConnectToMongoService();
                  Models.MongoHelper.Posts_collection =
                      Models.MongoHelper.database.GetCollection<Models.Posts>("Posts");
                  var filter = Builders<Models.User>.Filter.Eq("_id", id);

                  var update = Builders<Models.Posts>.Update
                      .Set("PostID", collection["PostID"])
                      .Set("Postcontent", collection["Postcontent"])
                      .Set("postsnikename", collection["postsnikename"])
                      .Set("Postscomment", collection["Postscomment"])
                      .Set("ComComment", collection["ComComment"])
                      .Set("PostLikes", collection["PostLikes"])
                      .Set("Gender", collection["Gender"]);
                  var result = Models.MongoHelper.Posts_collection.UpdateOneAsync(filter, update);


                  return RedirectToAction("Index");
             }
              catch
              {
                  return View();
              }
          }

         //GET: Post/Delete/5
          public ActionResult Delete(string id)
          {
              Models.MongoHelper.ConnectToMongoService();
              Models.MongoHelper.users_collection =
                  Models.MongoHelper.database.GetCollection<Models.User>("User");
              var filter = Builders<Models.User>.Filter.Eq("_id", id);
              var result = Models.MongoHelper.users_collection.Find(filter).FirstOrDefault();
              return View(result);
          }

        // POST: Posts/Delete/5
        [HttpPost]
public ActionResult Delete(string id, FormCollection collection)
           {
               try
               {
                    TODO: Add delete logic here
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

    
  */  }
}