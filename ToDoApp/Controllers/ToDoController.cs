using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoDbContext _todoDbContext;
        public ToDoController(ToDoDbContext toDoDbContext)
        {
            _todoDbContext = toDoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodos()
        {
            var todos = await _todoDbContext.ToDos
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
            return Ok(todos);
        }

        [HttpGet]
        [Route("get-deleted-todos")]
        public async Task<IActionResult> GetDeletedTodos()
        {
            var todos = await _todoDbContext.ToDos
                .Where (x => x.IsDeleted == true)
                .OrderByDescending (x => x.CreatedDate)
                .ToListAsync();
            return Ok(todos);
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo(ToDo todo)  
        {
            todo.Id = Guid.NewGuid();
            _todoDbContext.ToDos.Add(todo);
            await _todoDbContext.SaveChangesAsync();
            return Ok(todo);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateTodo([FromRoute] Guid id, ToDo todoUpdateRequest)
        {
            var todo = await _todoDbContext.ToDos.FindAsync(id);
            if(todo == null)
                return NotFound();
            todo.IsCompleted = todoUpdateRequest.IsCompleted;
            todo.CompleatedDate = DateTime.Now;
            await _todoDbContext.SaveChangesAsync();
            return Ok(todo);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] Guid id)
        {
            var todo = await _todoDbContext.ToDos.FindAsync(id);
            if(todo == null)
                return NotFound();
            todo.IsDeleted = true;
            todo.DeletedDate = DateTime.Now;
            await _todoDbContext.SaveChangesAsync();
            return Ok(todo);
        }

        [HttpPut]
        [Route("undo-deleted-todo/{id:Guid}")]

        public async Task<IActionResult> UndoDeletedTodo([FromRoute] Guid id)
        {
            var todo = await _todoDbContext.ToDos.FindAsync(id);
            if (todo == null)
                return NotFound();
            todo.DeletedDate = null;
            todo.IsDeleted = false;
            await _todoDbContext.SaveChangesAsync();
            return Ok(todo);

        }




    }
}
