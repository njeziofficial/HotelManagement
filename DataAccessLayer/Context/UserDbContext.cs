using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Context
{
    public class UserDbContext : IUserDbContext
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }

    public interface IUserDbContext
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
