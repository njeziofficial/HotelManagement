using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class MongoDbClient<T> : IMongoDbClient<T> where T : new()
    {
        private readonly IConfiguration _configuration;
        public MongoDbClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IMongoCollection<T> GetMongo() =>
        new MongoClient(_configuration
            .GetConnectionString("hotelDb"))
            .GetDatabase("HotelManagement")
            .GetCollection<T>(nameof(T).ToString());

    }
}
