namespace ToDoApp.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System.Linq;
    
    public class TodoRepository: ITodoRepository
    {
        private readonly ITodoContext _context;

        public TodoRepository(ITodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Todo>> GetAllTodos()
        {
            return await _context
                .Todos
                .Find(_ => true)
                .ToListAsync();
        }

        public Task<Todo> GetTodo(long id)
        {
            FilterDefinition<Todo> filter = Builders<Todo>.Filter.Eq(m => m.Id, id);
            return _context
                .Todos
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task Create(Todo todo)
        {
            // default every new todo to not complete.
            todo.IsComplete = false;
            
            await _context.Todos.InsertOneAsync(todo);
        }

        public async Task<bool> Update(Todo todo)
        {
            ReplaceOneResult updateResult = await _context
                .Todos
                .ReplaceOneAsync(
                    filter: g => g.Id == todo.Id,
                    replacement: todo);
            return updateResult.IsAcknowledged
                   && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(long id)
        {
            FilterDefinition<Todo> filter =
                Builders<Todo>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context
                .Todos
                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                   && deleteResult.DeletedCount > 0;
        }

        public async Task<long> GetNextId()
        {
            return await _context.Todos.CountDocumentsAsync(new BsonDocument()) + 1;
        }

        public async Task<Todo> UpdateCompletionStatus(long id, Todo todo)
        {
            FilterDefinition<Todo> filter = Builders<Todo>.Filter.Eq(m => m.Id, id);
            todo.IsComplete = !todo.IsComplete;
            var update = Builders<Todo>.Update.Set("IsComplete", todo.IsComplete);
            _context.Todos.UpdateOne(filter, update);
            return todo;
        }
    }
}