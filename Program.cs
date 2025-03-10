using Microsoft.EntityFrameworkCore;
using Restaurant.Models;
using Umbraco.Core.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages(); // 0310 加的
// Add services to the container.
//builder.Services.AddControllersWithViews();

//builder.Services.AddHttpContextAccessor();
//builder.Services.AddTransient<IUserRepository, IUserRepository>();
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie();

builder.Services.AddDbContext<HotPotContext>(
options => options.UseSqlServer(builder.Configuration.GetConnectionString("HotPotConnstring")));

//// Add services to the container. 使用 cookie 判斷是否是登入狀態
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
   .AddCookie(options =>
   {
       options.ExpireTimeSpan = TimeSpan.FromSeconds(20); //過期時間為20分鐘(秒)
       options.SlidingExpiration = true; //如果登入期間使用者有活動(例如發送請求),則重新計算過期時間
       options.LoginPath = "/Customers/Member_Login"; //未登入自動導至這個網址

       // 下面這些是為了讓跨站點的請求可以傳遞Cookie
       options.Cookie.HttpOnly = true; // 保護Cookie避免被JavaScript存取
       options.Cookie.SameSite = SameSiteMode.Lax; // 設定SameSite策略，避免跨站點問題
   });
//builder.Services.AddMvc(options =>
//{
//    options.Filters.Add(new AuthorizeFilter());//全部動作都須通過登入驗證才能使用
//});
// 加入授權
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // 開發環境顯示詳細錯誤資訊
}
else
{
    app.UseExceptionHandler("/Home/Error"); // 正式環境，轉向錯誤頁面
    app.UseHsts(); // 啟用 HSTS，提高安全性
}




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // 0310  先驗證
app.UseAuthorization(); // 再授權


app.MapRazorPages(); // 0310 
//app.MapDefaultControllerRoute(); // 0310 // 這個會設置 `{controller=Home}/{action=Index}/{id?}`
/*解決方法 如果你希望 /Customers/Member_Login 是預設頁面，刪除 MapDefaultControllerRoute();，保留 MapControllerRoute()。
或者，保持 MapDefaultControllerRoute();，但改變 HomeController 的 Index() 行為，讓它重定向到 Customers/Member_Login。*/
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customers}/{action=Member_Login}/{id?}");
// _Member  Register    Member_Login    create    Index_Member    Index
app.Run();
