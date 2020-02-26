using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class TodosController: Controller
    {
        private readonly ITodoRepository _repo;

        public TodosController(ITodoRepository repo)
        {
            _repo = repo;
        }
        
        //Get api/todos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> Get()
        {
            return new ObjectResult(await _repo.GetAllTodos());
        }
        
        //Get api/todos/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> Get(long id)
        {
            var todo = await _repo.GetTodo(id);

            if (todo == null)
                return new NotFoundResult();
            
            return new ObjectResult(todo);
        }
        
        //Post api/todos
        [HttpPost]
        public async Task<ActionResult<Todo>> Post([FromBody] Todo todo)
        {
            todo.Id = await _repo.GetNextId();
            await _repo.Create(todo);
            return new OkObjectResult(todo);
        }
        
        //Put api/todos
        [HttpPut("{Id}")]
        public async Task<ActionResult<Todo>> Put(long id, [FromBody] Todo todo)
        {
            var todoFromDb = await _repo.GetTodo(id);

            if (todoFromDb == null)
                return new NotFoundResult();

            todo.Id = todoFromDb.Id;
            todo.InternalId = todoFromDb.InternalId;

            await _repo.Update(todo);
            
            return new ObjectResult(todo);
        }
        
        //Delete api/todos/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var post = await _repo.GetTodo(id);

            if (post == null)
                return new NotFoundResult();

            await _repo.Delete(id);
            
            return new OkResult();
        }
    }
}