using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoteApplication.Areas.Identity.Data;
using NoteApplication.Business.Services;
using NoteApplication.Persistence.Contexts;
using NoteApplication.Persistence.Interfaces;
using NoteApplication.Persistence.Interfaces.Context;
using NoteApplication.Persistence.Interfaces.Services;
using NoteApplication.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDBContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDBContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<NoteDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<INoteContext>(provider => provider.GetService<NoteDBContext>());

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDBContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<INoteRepository, NoteRepository>();
builder.Services.AddTransient<INoteService, NoteServices>();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
