using Microsoft.VisualBasic.CompilerServices;

namespace ToDoApp.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    public class Todo
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsComplete { get; set; }
    }
    
    public class TodoPostRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}