using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoDBtest.Models
{
    public class User
    {
        public Object _id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Emailaddress { get; set; }
        public string Nickname { get; set; }
        public string Gender { get; set; }
    }
    
   
   
}