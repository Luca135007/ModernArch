using Microsoft.EntityFrameworkCore;
using ModernArch.Api.Models;

namespace ModernArch.Api.Data
{
    // 繼承 DbContext，這是 EF Core 的基底類別
    public class TodoContext : DbContext
    {
        // 建構子：接收設定選項 (例如連線字串)，傳給父類別
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }
        public DbSet<TodoItem> Todos { get; set; }

        // === 請新增下面這一段 (覆寫 OnModelCreating) ===
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. 告訴程式：資料表叫做 "TodoItems"
            modelBuilder.Entity<TodoItem>().ToTable("TodoItems");
            // 2. 告訴程式：Name 其實對應到資料庫的 Title
            modelBuilder.Entity<TodoItem>()
                .Property(t => t.Title)
                .HasColumnName("Title");  // <--- 關鍵修改

            // 3. 預防萬一，IsComplete 也可能叫 IsCompleted，我們先加上去保險
            modelBuilder.Entity<TodoItem>()
                .Property(t => t.IsCompleted)
                .HasColumnName("IsCompleted");
        }
        // ===========================================
        // 這就是你的資料表！
        // DbSet<TodoItem> 代表資料庫裡會有一張表叫做 TodoItems
       
    }
}
