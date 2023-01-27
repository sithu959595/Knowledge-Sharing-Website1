using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;

namespace MongoDBtest.Models
{
    public class MongoHelper
    {
        public static IMongoClient client { get; set; }
        public static IMongoDatabase database { get; set; }
        public static string MongoConnection = "mongodb+srv://Giles:kstest@knowledgesharing-ezszv.mongodb.net/test?retryWrites=true&w=majority";  //mlab提供的連線字串  
        public static string MongoDatabase = "KnowledgeSharing";

        public static IMongoCollection<Models.User> users_collection { get; set; }
        public static IMongoCollection<Models.Posts> Posts_collection { get; set; }
        public static IMongoCollection<Models.counters> counter_collection { get; set; }
        public static IMongoCollection<Models.Comments> Comments_collection { get; set; }

        public static void ConnectToMongoService()
        {
            //string MongoConnection = "mongodb+srv://Giles:kstest@knowledgesharing-ezszv.mongodb.net/test?retryWrites=true&w=majority";
            //string MongoDatabase = "KnowledgeSharing";
           try
            {
                client = new MongoClient(MongoConnection);
                database = client.GetDatabase(MongoDatabase);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*public static bool logincheck(string username, string password)
        {
            Models.MongoHelper.ConnectToMongoService();
            Models.MongoHelper.users_collection =
            Models.MongoHelper.database.GetCollection<Models.User>("User");

            var filterbyun = Builders<Models.User>.Filter.Eq("Username", username);
            var filterbypw = Builders<Models.User>.Filter.Eq("Password", password);
            var filter = filterbyun & filterbypw;

            //ViewBag.fil = filterbyun.ToList();
            //return View();
            var result = Models.MongoHelper.users_collection.Find(filter).ToList().FirstOrDefault();
            //if (MongoHelper.CheckUserData(collection["Username"], collection["Password"]) == true)
            return result == null;
        }*/
    }
}