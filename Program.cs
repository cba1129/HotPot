using Microsoft.EntityFrameworkCore;
using Restaurant.Models;
using Umbraco.Core.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();
//builder.Services.AddTransient<IUserRepository, IUserRepository>();


builder.Services.AddDbContext<HotPotContext>(
options => options.UseSqlServer(builder.Configuration.GetConnectionString("HotPotConnstring")));




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
    pattern: "{controller=Customers}/{action=Index}/{id?}");
// _Member  Register    Member_Login
app.Run();
