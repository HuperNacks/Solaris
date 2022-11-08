using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstate.Areas.Identity.Data;
using RealEstate.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();




var configuration = builder.Configuration.GetConnectionString("ApplicationDbContextConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration));


builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorizationPolicies();
builder.Services.AddScoped();

//Helpers.AddAuthorizationPolicies(builder.Services);

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
app.UseAuthentication(); ;

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "edit",
      pattern: "Admin/{action=Edit}/{id?}",
      defaults: new {controller = "Admin" , area = "Admin"}
    );
    endpoints.MapControllerRoute(
      name: "admin",
      pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});



app.MapRazorPages();

app.Run();


