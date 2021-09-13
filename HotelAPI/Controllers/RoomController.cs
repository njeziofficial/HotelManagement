using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Services.Abstract;
using Services.Concrete;
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
        private readonly IMongoCollection<Room> db;
        public RoomController(IRoomDbContext room)
        {
            var client = new MongoClient(room.ConnectionString);
            var database = client.GetDatabase(room.DatabaseName);

            db = database.GetCollection<Room>(room.CollectionName);
        }

        [HttpGet]
        public JsonResult ReviewRooms() =>
            new JsonResult(db.Find(rooms => rooms.RoomState != (int)RoomState.Unavailable)
            .ToList());

        [HttpGet("{id}")]
        public Room ReviewRoom(int id) =>
           db.Find(room => room.RoomID == id
           && room.RoomState != (int)RoomState.Unavailable)
                .FirstOrDefault();

        [HttpPost]
        public Room CreateRoom(Room room)
        {
            room.RoomID = (int)db.Find(rooms => true)
                .CountDocuments() + 1;
            db.InsertOne(room);
            return room;
        }

        [HttpPut("{id}")]
        public Room EditRoom(int id, Room room)
        {
            db.ReplaceOne(room => room.RoomID == id, room);
            return room;
        }

        [HttpDelete("{id}")]
        public string DeleteRoom(int id)
        {
            var room = db.Find(room => room.RoomID == id).FirstOrDefault();
            room.RoomState = (int)RoomState.Unavailable;
            db.ReplaceOne(room => room.RoomID == id, room);
            return "Room deleted successfully.";
        }
    }
}

