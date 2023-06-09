using ERP.DataAccess.Data;
using ERP.DataAccess.Repository;
using ERP.DataAccess.Repository.IRepository;
using ERP.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(opt=>
    {
        opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(90);
        opt.Lockout.MaxFailedAccessAttempts = 3;
});
builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Identity/Account/AccessDenied");
    opt.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Identity/Account/Login");
    
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAuthentication().AddGoogle(options =>
{
    options.ClientId = "626433691249-cbluhgul5vgd2dbbbjrjg496634n1l8v.apps.googleusercontent.com";
    options.ClientSecret = "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";
});

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

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Public}/{controller=Home}/{action=Index}/{id?}");

app.Run();
