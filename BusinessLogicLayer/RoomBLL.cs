using DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer
{
    public class RoomBLL
    {
        private readonly IConfiguration _configuration;
        public RoomBLL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //private IMongoCollection<Room> db() =>
        //new MongoClient(_configuration
        //    .GetConnectionString("hotelDb"))
        //    .GetDatabase("HotelManagement")
        //    .GetCollection<DataAccessLayer.Models.User>("User");

    }
}
