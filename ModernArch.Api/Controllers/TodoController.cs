using Microsoft.AspNetCore.Mvc;
//using ModernArch.Api.Models;
using ModernArch.Api.DTOs; // 改用 DTO
using ModernArch.Api.Services;

namespace ModernArch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: api/Todo
        [HttpGet]
        public ActionResult<IEnumerable<TodoItemDto>> GetTodos() // 改型別
        {
            return Ok(_todoService.GetAll());
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public ActionResult<TodoItemDto> GetTodoItem(int id) // 改型別
        {
            var todoItem = _todoService.GetById(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // POST: api/Todo
        [HttpPost]
        public ActionResult<TodoItemDto> PostTodoItem(TodoItemDto todoDto) // 改型別
        {
            var newTodo = _todoService.Add(todoDto);

            // 這裡回傳 201 Created，並附上新資源的網址
            return CreatedAtAction(nameof(GetTodoItem), new { id = newTodo.Id }, newTodo);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public IActionResult PutTodoItem(int id, TodoItemDto todoDto) // 改型別
        {
            if (id != todoDto.Id)
            {
                return BadRequest();
            }
            // 因為 Service 的 Update 已經處理了找不到的情況 (雖然只是 return)，
            // 這裡我們可以再檢查一次，或者讓 Service 回傳 bool。
            // 為了簡單，我們先假設如果 GetById 找不到就是 NotFound
            // 先檢查是否存在
            if (_todoService.GetById(id) == null)
            {
                return NotFound();
            }

            _todoService.Update(todoDto);

            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(int id)
        {
            var todoItem = _todoService.GetById(id);
            // 這邊邏輯跟之前差不多
            if (_todoService.GetById(id) == null)
            {
                return NotFound();
            }

            _todoService.Delete(id);

            return NoContent();
        }
    }
}
