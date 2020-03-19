using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    //[Authorize]
    //[ApiController]
    [Produces("application/json")]
    [Route("api/[Controller]")]

   /* public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel model)

        private var user = await _userService.Authenticate(model.Username); 
        If(user == null)
            return BadRequest(new {message = "Username or Password is not valid."});

        return ok(user);
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

    }*/
    
    [Authorize]
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
        public async Task<ActionResult<Todo>> Post([FromBody] TodoPostRequest todoPostBody)
        {
            
            var id = await _repo.GetNextId();
            var todo = new Todo()
            {
                Content = todoPostBody.Content,
                Title = todoPostBody.Title,
                Id = id,
                IsComplete = false,
            };
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
            
            return new NoContentResult();
        }
        
        //patch api/todos/1/completionStatus
        [HttpPatch("{id}/completionStatus")]
        public async Task<ActionResult<Todo>> Patch(long id)
        {
            var todoFromDb = await _repo.GetTodo(id);
            
            if (todoFromDb == null)
                return new NotFoundResult();
            
            return await _repo.UpdateCompletionStatus(id, todoFromDb);
        }
    }
}