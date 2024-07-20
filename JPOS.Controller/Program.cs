using JPOS.Model;
using BusinessObject.Entities;
using JPOS.Model.Models.AppConfig;
using JPOS.Service.Implementations;
using JPOS.Service.Interfaces;
using JPOS.Service.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using JPOS.Repository.Repositories.Interfaces;
using JPOS.DAO.EntitiesDAO;
using JPOS.Repository.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(typeof(ApplicationMapper));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
// Register Services


builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<ICategoryService, CatergoryService>();
builder.Services.AddScoped<IDesignService, DesignService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<ITransactionServices, TransactionServices>();
builder.Services.AddScoped<IProductMaterialService, ProductMaterialService>();
builder.Services.AddScoped<IDesignService, DesignService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ICategoryService, CatergoryService>();

builder.Services.AddHttpClient();

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
app.UseSession();
app.UseMiddleware<HandleRoleMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();
app.Run();
