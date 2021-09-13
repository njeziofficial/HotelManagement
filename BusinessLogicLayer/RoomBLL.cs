using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class RoomBLL
    {
        private readonly IMongoCollection<Room> db;
        public RoomBLL(IRoomDbContext room)
        {
            var client = new MongoClient(room.ConnectionString);
            var database = client.GetDatabase(room.DatabaseName);

            db = database.GetCollection<Room>(room.CollectionName);
        }

        public List<Room> ReviewRooms() =>
            db.Find(rooms => rooms.RoomState != (int)RoomState.Unavailable)
            .ToList();

        public Room ReviewRoom(int id) =>
           db.Find(room => room.RoomID == id 
           && room.RoomState != (int)RoomState.Unavailable)
                .FirstOrDefault();
        
        
        public Room CreateRoom(Room room)
        {
            room.RoomID = (int)db.Find(rooms => true)
                .CountDocuments() + 1;
            db.InsertOne(room);
            return room;
        }

        public Room EditRoom(int id, Room room)
        {
            db.ReplaceOne(room => room.RoomID == id, room);
            return room;
        }

        public string DeleteRoom(int id)
        {
            var room = db.Find(room => room.RoomID == id).FirstOrDefault();
            room.RoomState = (int)RoomState.Unavailable;
            db.ReplaceOne(room => room.RoomID == id, room);
            return "Room deleted successfully.";
        }

    }
}
