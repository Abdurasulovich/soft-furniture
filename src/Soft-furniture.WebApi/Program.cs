using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Soft_furniture.DataAccess.Interfaces.Catalogs;
using Soft_furniture.DataAccess.Interfaces.Delivers;
using Soft_furniture.DataAccess.Interfaces.Orders;
using Soft_furniture.DataAccess.Interfaces.Products;
using Soft_furniture.DataAccess.Interfaces.Types;
using Soft_furniture.DataAccess.Interfaces.Users;
using Soft_furniture.DataAccess.Repositories.Catalogs;
using Soft_furniture.DataAccess.Repositories.Deliveries;
using Soft_furniture.DataAccess.Repositories.Orders;
using Soft_furniture.DataAccess.Repositories.Products;
using Soft_furniture.DataAccess.Repositories.Types;
using Soft_furniture.DataAccess.Repositories.Users;
using Soft_furniture.Service.Interfaces.Auth;
using Soft_furniture.Service.Interfaces.AuthDelivers;
using Soft_furniture.Service.Interfaces.Catalogs;
using Soft_furniture.Service.Interfaces.Common;
using Soft_furniture.Service.Interfaces.Delivers;
using Soft_furniture.Service.Interfaces.Furniture_types;
using Soft_furniture.Service.Interfaces.Notifications;
using Soft_furniture.Service.Interfaces.Orders;
using Soft_furniture.Service.Interfaces.Products;
using Soft_furniture.Service.Interfaces.Users;
using Soft_furniture.Service.Services.Auth;
using Soft_furniture.Service.Services.AuthDelivers;
using Soft_furniture.Service.Services.Catalogs;
using Soft_furniture.Service.Services.Common;
using Soft_furniture.Service.Services.Delivers;
using Soft_furniture.Service.Services.Furniture_types;
using Soft_furniture.Service.Services.Notifications;
using Soft_furniture.Service.Services.Orders;
using Soft_furniture.Service.Services.Products;
using Soft_furniture.Service.Services.Users;
using Soft_furniture.WebApi.Configuretions;
using Soft_furniture.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.ConfigureJwtAuth();
builder.ConfigureSwaggerAuth();
builder.ConfigureCORSPolicy();


builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
builder.Services.AddScoped<ITypeRepository, TypeRepository>();
builder.Services.AddScoped<IDeliveryRepository, DeliverRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IAuthDeliverService, AuthDeliverService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<ITypeService, TypeService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IDeliverTokenService, DeliverTokenService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDeliverService, DeliverService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();


builder.Services.AddSingleton<ISmsSender, SmsSender>();
var app = builder.Build();

// Configure the HTTP request pipeline.

//development, staging, production
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseStaticFiles();
app.UseMiddleware<Soft_furniture.WebApi.Middlewares.ExceptionHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
