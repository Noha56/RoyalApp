using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RoyalFinalApp.areas.Payment.Helpers;
using RoyalFinalApp.Areas.Payment.Services;
using RoyalFinalApp.Data;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));
builder.Services.AddTransient<ISMService, SMService>();

StripeConfiguration.ApiKey=builder.Configuration["Stripe:Secretkey"];

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));


//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("Admin", new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .RequireClaim("role", "Admin")
//        .Build());
//});

builder.Services.AddMvc().AddNToastNotifyToastr(new NToastNotify.ToastrOptions
{
    ProgressBar= true,
    PositionClass=ToastPositions.BottomRight,
    PreventDuplicates=true,
    CloseButton=true,
});

//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(option =>
//{
//    option.IdleTimeout = TimeSpan.FromSeconds(20);
//    option.Cookie.HttpOnly = true;
//    option.Cookie.IsEssential = true;
//});

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
//app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
       name: "areas",
       pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
