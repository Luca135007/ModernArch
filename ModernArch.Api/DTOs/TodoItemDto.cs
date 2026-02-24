namespace ModernArch.Api.DTOs
{
    public class TodoItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        // 修改這一行：給它預設值，或是加問號
        public bool IsCompleted { get; set; }
        // 這裡沒有 SecretKey，所以絕對安全！
    }
}
