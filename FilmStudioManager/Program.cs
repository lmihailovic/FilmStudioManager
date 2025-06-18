using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FilmStudioManager.Data;
using FilmStudioManager.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MariaDbServerVersion(new Version(10, 6, 0))
    ));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await CreateAdminRoleAndUserAsync(services);
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

async Task CreateAdminRoleAndUserAsync(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    string roleName = "Admin";

        var roleExists = await roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            Console.WriteLine("🔥 Seeding role...");
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }


    var adminEmail = "admin@example.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        Console.WriteLine("🔥 Seeding admin user...");
        var admin = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail
        };

        string adminPassword = "Admin123!";

        var createAdmin = await userManager.CreateAsync(admin, adminPassword);
        if (createAdmin.Succeeded)
        {
            Console.WriteLine("🔥 Succeeded");

            await userManager.AddToRoleAsync(admin, "Admin");
        }
        else
        {
            foreach (var error in createAdmin.Errors)
            {
                Console.WriteLine($"❌ Failed to create admin: {error.Description}");
            }
        }
    }
}