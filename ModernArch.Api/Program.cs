// 1. 建立一個 "建造者 (Builder)"。
// 它會自動幫你載入 appsettings.json、環境變數等設定。
// 以前你要自己寫 ConfigurationManager 讀檔，現在這一行全包了。
using Microsoft.EntityFrameworkCore;
using ModernArch.Api.Data;
using ModernArch.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// --- 加入這段 CORS 設定 ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   // 允許任何來源 (正式環境不建議這樣，但在練習時很方便)
              .AllowAnyMethod()   // 允許任何 HTTP 方法 (GET, POST, PUT, DELETE)
              .AllowAnyHeader();  // 允許任何 Header
    });
});
// ------------------------



// Add services to the container.
// --- 下面這區塊叫做 "DI Container (服務容器)" ---
// 這裡的概念是：告訴系統「我有什麼能力」。
// 你在這裡註冊的任何東西，之後都可以在任何 Class 的建構子裡「討」來用。


// --- 修改這段：改用 SQLite ---
// 註冊 DbContext
// 使用 SQLite，跨平台支援（Windows、Linux、macOS、Codespaces）
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// ----------------

// 2. 告訴系統：「我要用 Controller 的方式來寫 API」。
// 如果沒加這行，你的 Controller 寫了也沒人理。
builder.Services.AddControllers();

// --- 註冊 Service ---
builder.Services.AddScoped<ITodoService, TodoService>();
// -------------------

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// 3. 這兩行是為了 Swagger (自動產生 API 文件網頁)。
// 以前寫 API 還要另外寫 Word 文件給前端，現在系統會自動掃描你的程式碼生成網頁。
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 4. 工具都準備好了，開始把 App 建立起來。
// 這一行之後，就不能再註冊 Service 了 (builder.Services... 無效)。
var app = builder.Build();

// Configure the HTTP request pipeline.

// 5. 判斷環境：如果是 "開發環境 (Development)"...
// 這是為了安全。你不會希望正式機上的駭客看到 Swagger 測試頁面。
if (app.Environment.IsDevelopment())
{
    // 6. 啟用 Swagger 中介軟體。
    // 這會產生一個漂亮的網頁，列出你所有的 API，還能直接在上面按 "Try it out" 測試。
    app.UseSwagger();
    app.UseSwaggerUI();
}

// --- 加入這行 ---
app.UseCors("AllowAll"); // 啟用 CORS 策略
// ----------------

// 7. 強制轉址 HTTPS。
// 如果有人用 http:// 連進來，自動把他踢去 https://。
// 在 Codespaces 或容器環境中，可能不需要這個（因為反向代理已處理）
// 可以透過環境變數控制是否啟用
if (!app.Environment.IsDevelopment() || 
    builder.Configuration.GetValue<bool>("UseHttpsRedirection", true))
{
    app.UseHttpsRedirection();
}


// 8. 授權檢查。
// 檢查這個人有沒有權限看這個頁面。(注意：這裡還沒執行 Controller 喔！)
app.UseAuthorization();


// ★★★ 加入這兩行 ★★★
app.UseDefaultFiles(); // 1. 讓網址不用打 index.html 也能自動找到它
app.UseStaticFiles();  // 2. 開放 wwwroot 資料夾裡的檔案讓外部存取
// 9. 對應控制器。
// 系統會去掃描你所有的 Controller，根據 [Route] 設定，決定這個 Request 該去哪裡。
// 就像大樓的管理員，看你的門牌號碼把你指引到正確的房間。
app.MapControllers();

// 10. 啟動引擎，開始監聽！
// 程式會停在這裡一直跑，直到你把視窗關掉。
app.Run();
