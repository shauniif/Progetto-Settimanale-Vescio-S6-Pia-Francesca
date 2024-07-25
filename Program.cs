using Microsoft.AspNetCore.Authentication.Cookies;
using Progetto_Settimanale_Vescio_Pia_Francesca;
using Progetto_Settimanale_Vescio_Pia_Francesca.DBContext;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Classes;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Password_Crypth_Implementations;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt => opt.LoginPath = "/Auth/FirstPage");

builder.Services
    .RegisterDAO()
    .AddScoped<DbContext>()
    .AddScoped<IAccountService, AccountService>()
    .AddScoped<IPasswordEncoder, PasswordEnconders>()
    .AddScoped<ICustomerService, CustomerService>()
    .AddScoped<IRoomService, RoomService>()
    .AddScoped<IAdditionalSvcService, AdditionalSvcService>()
    .AddScoped<IBookingService, BookingService>()
    .AddScoped<IBookingSvcService, BookingSvcService>()
    .AddScoped<ICheckOutService, CheckOutService>()
    .AddControllersWithViews();
builder.Services.
              AddAuthorization(opt =>
              {
                  opt.AddPolicy(Policies.LoggedIn, cfg => cfg.RequireAuthenticatedUser());
                  opt.AddPolicy(Policies.IsAdmin, cfg => cfg.RequireRole("Admin"));
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
