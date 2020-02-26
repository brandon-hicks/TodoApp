namespace ToDoApp.Models
{
    using MongoDB.Driver;
    
    public interface ITodoContext
    {
        IMongoCollection<Todo> Todos { get; }
    }
}