using Athanasia.Data;
using Athanasia.Helpers;
using Athanasia.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("PERMISOSELEVADOS",
//        policy => policy.RequireRole("Psiquiatría", "Cardiología"));
//    options.AddPolicy("ADMIN",
//        policy => policy.RequireClaim("Administrador"));
//});
builder.Services.AddAntiforgery();
builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(
    CookieAuthenticationDefaults.AuthenticationScheme,
    config => config.AccessDeniedPath = "/Managed/ErrorAcceso");
builder.Services.AddControllersWithViews
    (options => options.EnableEndpointRouting = false)
    .AddSessionStateTempDataProvider();
builder.Services.AddTransient<HelperPathProvider>();
builder.Services.AddTransient<HelperMails>();
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddTransient<RepositoryAthanasia>();
string connectionString = builder.Configuration.GetConnectionString("SqlServerAthanasia");
builder.Services.AddDbContext<AthanasiaContext>(
    options => options.UseSqlServer(connectionString)
    );
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
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controller=Libro}/{action=Index}/{id?}");
});

app.Run();
