using Microsoft.AspNetCore.Mvc;

namespace ModernArch.Api.Controllers
{
    // [ApiController]: 告訴系統這是一個 API 控制器，它會自動幫你做參數檢查等雜事。
    [ApiController]
    // [Route]: 這是路由規則。"[controller]" 代表會自動用類別名稱去掉 Controller 後的字當網址。
    // 所以這個 API 的網址會是 /Hello
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        // [HttpGet]: 告訴系統，當有人用 GET 方法呼叫 /Hello 時，執行這個函式。
        [HttpGet]
        public IActionResult Get()
        {
            // 以前我們要自己組 JSON 字串，現在不用了。
            // 只要用匿名物件 (new { ... })，系統會自動幫你序列化成 JSON。
            var data = new
            {
                Message = "這是我第一支現代化 API",
                Time = DateTime.Now,
                Developer = "未來的架構師"
            };

            // Ok() 代表 HTTP 200 Success。
            return Ok(data);
        }
    }
}
