using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using BusinessLogicLayer.States;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccessLayer.Models
{
    public class Room
    {
        public ObjectId Id { get; set; }
        public int RoomNumber { get; set; }
        public int RoomState { get; set; }
        public int RoomClassesID { get; set; }
        public int RoomServicesID { get; set; }
        public int UserID { get; set; }
        public double Price { get; set; }
        public int RoomID { get; set; }
    }
}
