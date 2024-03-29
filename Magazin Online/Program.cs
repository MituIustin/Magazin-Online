using System.Globalization;
using Magazin_Online.Data;
using Magazin_Online.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");


app.MapControllerRoute(
    name: "ShowProduct",
    pattern: "Product/Show/{id}",
    defaults: new { controller = "Product", action = "Show" });

app.MapControllerRoute(
    name: "AcceptProduct",
    pattern: "Product/Accept/{id}",
    defaults: new { controller = "Product", action = "Accept" });

app.MapControllerRoute(
    name: "DeleteProduct",
    pattern: "Product/Delete/{id}",
    defaults: new { controller = "Product", action = "Delete" });


app.MapControllerRoute(
    name: "DeleteCategory",
    pattern: "Category/Delete/{id}",
    defaults: new { controller = "Category", action = "Delete" });

app.MapControllerRoute(
    name: "EditCategory",
    pattern: "Category/Edit/{id}",
    defaults: new { controller = "Category", action = "Edit" });

app.MapControllerRoute(
    name: "NewBasket",
    pattern: "BasketProduct/Add/{id_prod}",
    defaults: new { controller = "BasketProduct", action = "Add" });

app.MapControllerRoute(
    name: "EditProduct",
    pattern: "Product/Edit/{id}",
    defaults: new { controller = "Product", action = "Edit" });

app.MapControllerRoute(
    name: "NewBasket",
    pattern: "Basket/New/",
    defaults: new { controller = "Basket", action = "New" });


app.MapControllerRoute(
    name: "SortC",
    pattern: "Product/pretcresc/{searched?}",
    defaults: new { controller = "Product", action = "pretcresc" });

app.MapControllerRoute(
    name: "SortD",
    pattern: "Product/pretdescresc/{searched?}",
    defaults: new { controller = "Product", action = "pretdescresc" });

app.MapControllerRoute(
    name: "DeleteFromBasket",
    pattern: "BasketProduct/Delete/{id_prod}",
    defaults: new { controller = "BasketProduct", action = "Delete" });


app.MapControllerRoute(
    name: "PlaceOrder",
    pattern: "Order/New/{AdresaLivrare}",
    defaults: new { controller = "Order", action = "New" });

app.MapControllerRoute(
    name: "DeleteComment",
    pattern: "Comment/Delete/{id}",
    defaults: new { controller = "Comment", action = "Delete" });

app.MapControllerRoute(
    name: "EditComment",
    pattern: "Comment/Edit/{id}",
    defaults: new { controller = "Comment", action = "Edit" });
app.MapRazorPages();

app.MapControllerRoute(
    name: "EditReview",
    pattern: "Review/Edit/{id}",
    defaults: new { controller = "Review", action = "Edit" });

app.MapControllerRoute(
    name: "GiveRole",
    pattern: "Administration/GiveRole/{id}/{rol}",
    defaults: new { controller = "Administration", action = "GiveRole" });




app.Run();
