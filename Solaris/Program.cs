using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Solaris.Core.Entities;
using Solaris.Extensions;
using Solaris.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(option =>
{
    option.SuppressAsyncSuffixInActionNames = false;
});




var configuration = builder.Configuration.GetConnectionString("ApplicationDbContextConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration));


builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorizationPolicies();
builder.Services.AddScoped();
builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Login");

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.FromMinutes(10);
});

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
      name: "Dashboard",
      pattern: "Admin/{action=Dashboard}",
      defaults: new {controller = "Admin" , area = "Admin"}
    );
    endpoints.MapControllerRoute(
      name: "admin",
      pattern: "{area:exists}/{controller=Admin}/{action=UsersManager}/{id?}"
    );
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});



app.MapRazorPages();

app.Run();


