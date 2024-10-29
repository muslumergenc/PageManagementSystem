using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PageManagementSystem.Application.Interfaces;
using PageManagementSystem.Application.Services;
using PageManagementSystem.Infrastructure.Data;
using PageManagementSystem.Infrastructure.Interfaces;
using PageManagementSystem.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connStr));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient<IEmailSenderService, EmailSender>();
builder.Services.AddScoped<IPageService, PageService>();
builder.Services.AddScoped<IPageDataService, PageDataService>();
builder.Services.AddScoped<IPageContentService,PageContentService>();

builder.Services.AddScoped<IPageRepository, PageRepository>();
builder.Services.AddScoped<IPageDataRepository, PageDataRepository>();
builder.Services.AddScoped<IPageContentRepository, PageContentRepository>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/Login/AccessDenied";
});
var app = builder.Build();


//using (var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//    if (!await roleManager.RoleExistsAsync("Customer"))
//    {
//        await roleManager.CreateAsync(new IdentityRole("Customer"));
//    }
//}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization(); 
app.MapControllerRoute(name:"default",pattern:"{controller=Home}/{action=Index}/{id?}");
app.Run();
