using Microsoft.AspNetCore.Authentication.Cookies;
using System.Reflection;
using TaskManager.Infrastructure.Constants;
using TaskManager.Infrastructure.Contracts;
using TaskManager.Infrastructure.Services;
using TaskManager.UI.Services;
using TaskManager.UI.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ITaskManagerService, TaskManagerHttpService>();
builder.Services.AddScoped<IUserService, UserHttpService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();

builder.Services.AddHttpClient(ApiPaths.TaskManagerApiName, client =>
{
    client.BaseAddress = new Uri("https://localhost:7166/");
    // Configure other settings as needed
});

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
