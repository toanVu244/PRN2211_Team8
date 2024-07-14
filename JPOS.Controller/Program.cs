using JPOS.Model.Repositories.Interfaces;
using JPOS.Model;
using JPOS.Service.Implementations;
using JPOS.Service.Interfaces;
using JPOS.Model.Entities;
using Microsoft.EntityFrameworkCore;
using JPOS.Service.Tools;
using JPOS.Model.Models.AppConfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(ApplicationMapper));
builder.Services.AddDbContext<JPOS_ProjectContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DB"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserServices, UserServices>(); 
builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddSingleton(builder.Configuration.GetSection("Jwt").Get<AppConfig>());
var appConfig = builder.Configuration.GetSection("Jwt").Get<AppConfig>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();


app.Run();
