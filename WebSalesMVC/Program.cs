using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebSalesMVC.Data;
using WebSalesMVC.Services;
var builder = WebApplication.CreateBuilder(args);
var a = builder.Configuration.GetConnectionString("WebSalesMVCContext");
builder.Services.AddDbContext<WebSalesMVCContext>(options =>
    options.UseMySql(a, ServerVersion.AutoDetect(a), b => b.MigrationsAssembly("WebSalesMVC")));
builder.Services.AddScoped<SeedingService>();
builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<DepartmentService>();

// Add services to the container.
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
