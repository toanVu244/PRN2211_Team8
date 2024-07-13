using JPOS.Model.Repositories.Interfaces;
using JPOS.Model;
using JPOS.Service.Implementations;
using JPOS.Service.Interfaces;
using JPOS.Model.Entities;
using Microsoft.EntityFrameworkCore;
using JPOS.Service.Tools;
using JPOS.Model.Models.AppConfig;
using JPOS.Model.Repositories.Implementations;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSession();
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(typeof(ApplicationMapper));

builder.Services.AddDbContext<JPOS_ProjectContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DB"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<ICategoryService, CatergoryService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddHttpClient();

builder.Services.AddSingleton(builder.Configuration.GetSection("Jwt").Get<AppConfig>());
var appConfig = builder.Configuration.GetSection("Jwt").Get<AppConfig>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();
app.MapControllers();
app.MapRazorPages();

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An unexpected error occurred.");
        throw;
    }
});

app.Run();
