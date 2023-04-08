using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using OneKey.Shared.Utilities;
using OneKey.ServiceClient;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Newtonsoft.Json;
using OneKey.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OneKey.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

builder.Services.AddControllersWithViews()
    .AddViewLocalization(
        LanguageViewLocationExpanderFormat.Suffix,
        opts => { opts.ResourcesPath = "Resources";})
    .AddDataAnnotationsLocalization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers().AddNewtonsoftJson(o => 
{
    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Services.Configure<RequestLocalizationOptions>(
    opts =>
    {
        var supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("en-GB"),
            new CultureInfo("en-US"),
            new CultureInfo("en"),
            new CultureInfo("fr-FR"),
            new CultureInfo("fr"),
        };
        opts.DefaultRequestCulture = new RequestCulture("en-GB");
        opts.SupportedCultures = supportedCultures;
        opts.SupportedUICultures = supportedCultures;
    });

// Service Client List
builder.Services.AddScoped(typeof(IIdentityResolver), typeof(IdentityResolver));
builder.Services.AddScoped(typeof(ITokenResolver), typeof(TokenResolver));
builder.Services.AddScoped(typeof(IPayloadResolver), typeof(PayloadResolver));
builder.Services.AddScoped(typeof(IBaseServiceClient<>), typeof(BaseServiceClient<>));
builder.Services.AddScoped(typeof(IUserServiceClient), typeof(UserServiceClient));
builder.Services.AddScoped(typeof(IPasswordServiceClient), typeof(PasswordServiceClient));

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    o.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    o.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(o =>
    {
        o.Cookie.Name = "OneKey.Cookie";
        o.LoginPath = "/User/Login";
    });

builder.Services.AddSession();

builder.Services.AddHttpClient(OneKeyWebConstants.ClientScope, c =>
{
    c.BaseAddress = new Uri(OneKeyApiConstants.HostUrl);
});

var app = builder.Build();

app.UseLoginMiddleware();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

var options = ((IApplicationBuilder)app).ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();

app.UseRequestLocalization(options.Value);

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
