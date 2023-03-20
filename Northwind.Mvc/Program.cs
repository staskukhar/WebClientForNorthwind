// Section 1 - import namespaces
using Microsoft.AspNetCore.Identity; // Indentity users
using Microsoft.EntityFrameworkCore; // For Sqlite or MS SQL SERVER
using Northwind.Mvc.Data; //DB Context
using Packt.Shared; // Entities for EF Core
// Other imports - obj\Debug\net7.0\Northwind.Mvc.GlobalUsings.g.cs.

// Section 2 - configure the host web server including services
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

//string? sqlServerConnection = builder.Configuration
//    .GetConnectionString("NorthwindConnection");

//if (sqlServerConnection is null)
//{
//    Console.WriteLine("SQL Server database connection string is missing!");
//}
//else
//{
//    builder.Services.AddNorthwindContext(sqlServerConnection);
//}

builder.Services.AddNorthwindContext();

var app = builder.Build();


// Section 3 - Configure the HTTP request pipeline.
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

// Section 4 - start the host web server listening for HTTP requests
app.Run(); // blocking call
