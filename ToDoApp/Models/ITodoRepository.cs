namespace ToDoApp.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    public interface ITodoRepository
    {
        //api/[Get]
        Task<IEnumerable<Todo>> GetAllTodos();
        
        //api/1/[Get]
        Task<Todo> GetTodo(long id);
        
        //api/[Post]
        Task Create(Todo todo);
        
        //api/[Put]
        Task<bool> Update(Todo todo);
        
        //api/1/[Delete]
        Task<bool> Delete(long id);

        Task<long> GetNextId();
        
        //api/1/completionStatus/[Patch]
        Task<Todo> UpdateCompletionStatus(long id, Todo todo);
    }
}