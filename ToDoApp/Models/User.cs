using System.Security.Policy;
using Microsoft.VisualBasic.CompilerServices;

namespace ToDoApp.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class User
    {
        [BsonId]
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        


    }
}