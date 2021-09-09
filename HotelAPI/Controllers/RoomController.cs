using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RoomController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IMongoCollection<Room> db() =>
        new MongoClient(_configuration
            .GetConnectionString("hotelDb"))
            .GetDatabase("HotelManagement")
            .GetCollection<Room>("Room");
        

        // GET: api/<RoomController>
        [HttpGet]
        public JsonResult Get()
        {
            var rooms = db().AsQueryable();
            return new JsonResult(rooms);
        }

        // GET api/<RoomController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RoomController>
        [HttpPost]
        public JsonResult Post([FromBody] Room room)
        {
            int lastRoomID = db().AsQueryable().Count();
            room.RoomID = lastRoomID + 1;

            db().InsertOne(room);
            return new JsonResult("Room Added Successfully");

        }

        // PUT api/<RoomController>/5
        [HttpPut]
        public JsonResult Put(Room room)
        {
            var filter = Builders<Room>.Filter.Eq("RoomID", room.RoomID);
            var updatePrice = Builders<Room>.Update.Set("Price", room.Price);
            
            db().UpdateOne(filter, updatePrice);
            return new JsonResult("Updated Successfully");
        }

        // DELETE api/<RoomController>/5
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var filter = Builders<Room>.Filter.Eq("RoomID", id);

            db().DeleteOne(filter);
            return new JsonResult("Deleted Successfully");
        }
    }
}
