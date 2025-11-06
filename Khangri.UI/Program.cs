using Khangri.DataAccess.Abstract;
using Khangri.DataAccess.KhangriDAL;
using Khangri.DataAccess;
using Khangri.Entities;
using Khangri.UI.Helpers;
using Khangri.UI.Services;
using Khangri.UI.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(10);
        options.SlidingExpiration = true;
        options.LoginPath = "/Account/login";
        options.AccessDeniedPath = "/Forbiden";
    });
builder.Services.AddDataProtection().SetDefaultKeyLifetime(TimeSpan.FromDays(7))
               .PersistKeysToFileSystem(new DirectoryInfo(@".\Uploads\Keys"));
builder.Services.Configure<Khangri.Entities.ConnectionInfo>(builder.Configuration.GetSection("ConnectionInfo"));
builder.Services.Configure<MiscSettings>(builder.Configuration.GetSection("MiscSettings"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<UsernameHelper>();
builder.Services.AddScoped<IResetPasswordHelper, ResetPasswordHelper>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ISqlHelper, SqlHelper>();
builder.Services.AddScoped<IUtility, Khangri.UI.Utils.Utility>();
builder.Services.AddScoped<IBaLocation, BaLocation>();
builder.Services.AddScoped<ILocationHelper, LocationHelper>();
builder.Services.AddScoped<IBaImage, BaImage>();
builder.Services.AddScoped<IBaSightSeeing, BaSightSeeing>();
builder.Services.AddScoped<IBaTourType, BaTourType>();
builder.Services.AddScoped<IBaTour, BaTour>();
builder.Services.AddScoped<ITourTypeHelper, TourTypeHelper>();
builder.Services.AddScoped<ITourHelper, TourHelper>();
builder.Services.AddScoped<IImageHelper, ImageHelper>();
builder.Services.AddScoped<ISliderImageHelper, SliderImageHelper>();
builder.Services.AddScoped<IBaTourItinerary, BaTourItinerary>();
builder.Services.AddScoped<ITourItineraryHelper, TourItineraryHelper>();
builder.Services.AddScoped<IBaTourImage, BaTourImage>();
builder.Services.AddScoped<ITourImageHelper, TourImageHelper>();
builder.Services.AddScoped<IBaCMSContent, BaCMSContent>();
builder.Services.AddScoped<IBaCMSPage, BaCMSPage>();
builder.Services.AddScoped<IBaContact, BaContact>();
builder.Services.AddScoped<IBaCounter, BaCounter>();
builder.Services.AddScoped<IBaFeature, BaFeature>();
builder.Services.AddScoped<IBaSliderImages, BaSliderImages>();
builder.Services.AddScoped<IFeatureHelper, FeatureHelper>();
builder.Services.AddScoped<ICounterHelper, CounterHelper>();
builder.Services.AddScoped<IContactHelper, ContactHelper>();
builder.Services.AddScoped<IBaLocationDetails, BaLocationDetails>();
builder.Services.AddScoped<IBaNewLatterContact, BaNewLatterContact>();
builder.Services.AddScoped<IBaSetUp, BaSetUp>();
builder.Services.AddScoped<ISetUpHelper, SetUpHelper>();
builder.Services.AddScoped<IBaTeamMember, BaTeamMember>();
builder.Services.AddScoped<IBaLocationSightSeeing, BaLocationSightSeeing>();
builder.Services.AddScoped<IBaLocationImage, BaLocationImage>();
builder.Services.AddScoped<IBaTourTermsCondition, BaTourTermsCondition>();
builder.Services.AddScoped<IBaWEBTourDetails, BaWEBTourDetails>();
builder.Services.AddScoped<IBaWEBTourList, BaWEBTourList>();
builder.Services.AddScoped<IBaUserType, BaUserType>();
builder.Services.AddScoped<IBaUser, BaUser>();
builder.Services.AddScoped<IUserTypeHelper, UserTypeHelper>();
builder.Services.AddScoped<IUserHelper, UserHelper>();
builder.Services.AddScoped<IBaHotel, BaHotel>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
     name: "default",
     pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
