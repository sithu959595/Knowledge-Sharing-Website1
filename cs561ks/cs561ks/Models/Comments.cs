using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoDBtest.Models
{
    public class Comments
    {
        // public Object _id { get; set; }
        public Object _id { get; set; }
        public int Post_id { get; set; }
        public string Content { get; set; }
    }
}