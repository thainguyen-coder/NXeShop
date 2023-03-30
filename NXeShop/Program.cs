using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using NXeShop.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add AddRazorRuntimeCompilation 
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var connectionString = builder.Configuration.GetConnectionString("dbNXeShop");
builder.Services.AddDbContext<EWEBBANHANGDBDBMARKETSMDFContext>(x => x.UseSqlServer(connectionString));

// Add NotyF
builder.Services.AddNotyf(config => 
                            { config.DurationInSeconds = 10; 
                                config.IsDismissable = true; 
                                config.Position = NotyfPosition.TopRight; });

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
app.UseHttpLogging();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    // admin
    endpoints.MapControllerRoute(
                 name: "areas",
                 pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
               );
    // user
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

});

app.Run();
