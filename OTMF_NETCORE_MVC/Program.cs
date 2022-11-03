using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using OTMF_NETCORE_MVC.Hubs;
using OTMF_NETCORE_MVC.MiddlewareExtensions;
using OTMF_NETCORE_MVC.Models;
using OTMF_NETCORE_MVC.Reportes;
using OTMF_NETCORE_MVC.Services;
using OTMF_NETCORE_MVC.SuscribeTableDependencies;
using OTMF_NETCORE_MVC.Tools;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DashboardHub>();
builder.Services.AddSingleton<SuscribeOrdenTrabajoTableDependecy>();
builder.Services.AddSingleton<IServicioUsuarios, ServicioUsuarios>();
//builder.Services.AddSingleton<IReporteProduccion, ReporteProduccion>();
builder.Services.AddDbContext<OTMFContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));
var politicaUsuariosAutenticados = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
builder.Services.AddControllersWithViews(opciones =>
{
    opciones.Filters.Add(new AuthorizeFilter(politicaUsuariosAutenticados)); //politica de autorizacion de usuarios
});

builder.Services.AddHttpContextAccessor();
//builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<IRepositorioUsuarios, RepositorioUsuarios>();
builder.Services.AddTransient<IServicioUsuarios, ServicioUsuarios>();
builder.Services.AddTransient<IReporteProduccion, ReporteProduccion>();
builder.Services.AddTransient<IUserStore<Usuario>, UsuarioStore>();
builder.Services.AddTransient < SignInManager<Usuario>>();
builder.Services.AddIdentityCore<Usuario>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
}).AddCookie(IdentityConstants.ApplicationScheme, opciones =>
{
    opciones.LoginPath = "/usuarios/login";
    opciones.ExpireTimeSpan = TimeSpan.FromHours(12);
    opciones.AccessDeniedPath = "/Home/Privacy";
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAmin", policy => policy.RequireRole("Administrador"));
});
builder.Services.AddSignalR();
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
app.MapHub<DashboardHub>("/dashboardHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseProductTableDependency();
app.Run();
