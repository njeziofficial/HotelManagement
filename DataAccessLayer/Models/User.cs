using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class User
    {
        public ObjectId Id{ get; set; }
        public int ClientState { get; set; }
        public int UserID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int UserTypeID { get; set; }
        public bool isRegistered { get; set; } = false;
        public bool isEmailSent { get; set; } = false;
    }
}
