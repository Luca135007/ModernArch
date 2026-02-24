using Microsoft.EntityFrameworkCore;
using ModernArch.Api.Data;
using ModernArch.Api.DTOs; // 引用 DTO
using ModernArch.Api.Models;

namespace ModernArch.Api.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoContext _context;

        public TodoService(TodoContext context)
        {
            _context = context;
        }

        // 輔助方法：把 Model 轉成 DTO
        private static TodoItemDto ItemToDto(TodoItem todo) =>
            new TodoItemDto
            {
                Id = todo.Id,
                // 這裡改一下：如果 todo.Name 是 null，就給它空字串
                Title = todo.Title ?? string.Empty,
                IsCompleted = todo.IsCompleted
            };

        public IEnumerable<TodoItemDto> GetAll()
        {
            return _context.Todos
                .Select(x => new TodoItemDto
                {
                    Id = x.Id,
                    Title = x.Title, // <--- 把資料庫的  Title 對應給 DTO 的 Title
                    IsCompleted = x.IsCompleted
                })
                .ToList();
        }

        public TodoItemDto? GetById(int id)
        {
            var todo = _context.Todos.Find(id);
            return todo == null ? null : ItemToDto(todo);
        }

        public TodoItemDto Add(TodoItemDto todoDto)
        {
            // 把 DTO 轉回 Model 存進資料庫
            var todo = new TodoItem
            {
                Title = todoDto.Title,
                IsCompleted = todoDto.IsCompleted
            };

            _context.Todos.Add(todo);
            _context.SaveChanges();

            // 把產生的 ID 填回 DTO 回傳
            todoDto.Id = todo.Id;
            return todoDto;
        }

        public void Update(TodoItemDto todoDto)
        {
            var todo = _context.Todos.Find(todoDto.Id);
            if (todo == null) return;

            // 更新欄位
            todo.Title = todoDto.Title;
            todo.IsCompleted = todoDto.IsCompleted;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var todo = _context.Todos.Find(id);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
                _context.SaveChanges();
            }
        }
    }
}
