using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BookstoreApp.Data;
using BookstoreApp.Filters;

var builder = WebApplication.CreateBuilder(args);

// Configure SQLite Database
builder.Services.AddDbContext<BookstoreContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity Services with Roles
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>() // Enable Role Management
    .AddEntityFrameworkStores<BookstoreContext>();

// ✨ Add HTTP Request Logging Filter Globally
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<RequestLoggingFilter>(); // Logs all requests and responses
});

builder.Services.AddRazorPages();

var app = builder.Build();

// Create roles on startup
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// Enable Middleware Services in Correct Order
app.UseHttpsRedirection();
app.UseStaticFiles();  // ✅ Ensure static files are served **before routing**
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Map Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Required for Identity UI

app.Run();
