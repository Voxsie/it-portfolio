using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.DataAccess;
using Portfolio.Entity;
using Portfolio.Misc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<EmailSender>();
builder.Services.AddSingleton(builder.Configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfiguration>());
builder.Services.AddDbContext<Context>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));
builder.Services
    .AddIdentity<User, IdentityRole>(options => 
        options.User.AllowedUserNameCharacters.Contains(" "))
    .AddEntityFrameworkStores<Context>();

builder.Services.AddControllersWithViews();



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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();