using IoC;
using Microsoft.AspNetCore.Authentication.Cookies;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/SignIn/Index";
    //option.AccessDeniedPath = "/SignIn";
    //option.LogoutPath = "/SignOut";
    //option.Cookie.Name = "AuthCookie";
    //option.Cookie.HttpOnly = true;
    //option.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    //option.Cookie.SameSite = SameSiteMode.Strict;
    //option.Cookie.MaxAge = TimeSpan.FromDays(30);
    option.SlidingExpiration = true;
    option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
});

builder.Services.DependencyInjection(builder.Configuration);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SignIn}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
