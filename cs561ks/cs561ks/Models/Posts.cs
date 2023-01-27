using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoDBtest.Models
{
    
    public class Posts
    {
        public int _id { get; set; }
        public string Postscontent { get; set; }
        public DateTime Poststime { get; set; }
        public string Postsnikename { get; set; }
        public string Postscomment { get; set; }
        public string ComComment { get; set; } // comments for the comment
        public Int32 PostLikes { get; set; }



        public Posts()
        {
            Poststime = DateTime.Now;
          //  PostID = PostID + 1;
        }
    }
}