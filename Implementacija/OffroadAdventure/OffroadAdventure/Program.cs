using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OffroadAdventure.Data;
using OffroadAdventure.OpenRouteServiceAPI;
using OffroadAdventure.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString(
"DefaultConnection") ?? throw new InvalidOperationException("Connection string'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddHttpClient<OpenRouteServiceClient>();
builder.Services.Configure<ORSOptions>(builder.Configuration.GetSection("ORS"));

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password = new PasswordOptions
    {
        RequireDigit = false,
        RequiredLength = 5,
        RequireLowercase = false,
        RequireUppercase = false,
        RequireNonAlphanumeric = false,
    };
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();
