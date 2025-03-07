using Microsoft.EntityFrameworkCore;
using Restaurant.Models;
using Umbraco.Core.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();
//builder.Services.AddTransient<IUserRepository, IUserRepository>();


builder.Services.AddDbContext<HotPotContext>(
options => options.UseSqlServer(builder.Configuration.GetConnectionString("HotPotConnstring")));

//// Add services to the container. �ϥ� cookie �P�_�O�_�O�n�J���A
//builder.Services.AddControllersWithViews();
//builder.Services.AddHttpContextAccessor();
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//   .AddCookie(options =>
//   {
//       options.ExpireTimeSpan = TimeSpan.FromMinutes(20); //�L���ɶ���20����
//       options.SlidingExpiration = true; //�p�G�n�J�����ϥΪ̦�����(�Ҧp�o�e�ШD),�h���s�p��L���ɶ�
//       options.LoginPath = "/Home"; //���n�J�۰ʾɦܳo�Ӻ��}
//   });
//builder.Services.AddMvc(options =>
//{
//    options.Filters.Add(new AuthorizeFilter());//�����ʧ@�����q�L�n�J���Ҥ~��ϥ�
//});




var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customers}/{action=Member_Login}/{id?}");
// _Member  Register    Member_Login    create    Index_Member    Index
app.Run();
