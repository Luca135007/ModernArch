using ModernArch.Api.DTOs; // 引用 DTO
using ModernArch.Api.Models;

namespace ModernArch.Api.Services
{
    public interface ITodoService
    {
        IEnumerable<TodoItemDto> GetAll(); // 改這裡
        TodoItemDto? GetById(int id);      // 改這裡
        TodoItemDto Add(TodoItemDto todoDto); // 輸入輸出都改成 DTO
        void Update(TodoItemDto todoDto);     // 輸入改成 DTO
        void Delete(int id);
    }
}
