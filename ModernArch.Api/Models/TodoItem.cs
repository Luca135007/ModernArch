namespace ModernArch.Api.Models
{
    public class TodoItem
    {
        public int Id { get; set; } // EF Core 會自動把這欄位當作 Primary Key (主鍵)
        public string Title { get; set; }  // <--- 從 Name 改成 Title
        public bool IsCompleted { get; set; }
    }
}
