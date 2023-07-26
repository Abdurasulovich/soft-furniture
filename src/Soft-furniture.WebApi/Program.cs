using Soft_furniture.DataAccess.Interfaces.Catalogs;
using Soft_furniture.DataAccess.Interfaces.Products;
using Soft_furniture.DataAccess.Interfaces.Types;
using Soft_furniture.DataAccess.Interfaces.Users;
using Soft_furniture.DataAccess.Repositories.Catalogs;
using Soft_furniture.DataAccess.Repositories.Products;
using Soft_furniture.DataAccess.Repositories.Types;
using Soft_furniture.DataAccess.Repositories.Users;
using Soft_furniture.Service.Interfaces.Catalogs;
using Soft_furniture.Service.Interfaces.Common;
using Soft_furniture.Service.Interfaces.Furniture_types;
using Soft_furniture.Service.Interfaces.Products;
using Soft_furniture.Service.Interfaces.Users;
using Soft_furniture.Service.Services.Catalogs;
using Soft_furniture.Service.Services.Common;
using Soft_furniture.Service.Services.Furniture_types;
using Soft_furniture.Service.Services.Products;
using Soft_furniture.Service.Services.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();


builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
builder.Services.AddScoped<ITypeRepository, TypeRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<ITypeService, TypeService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
var app = builder.Build();

// Configure the HTTP request pipeline.

//development, staging, production
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
app.Run();
