using Soft_furniture.DataAccess.Interfaces.Catalogs;
using Soft_furniture.DataAccess.Repositories.Catalogs;
using Soft_furniture.Service.Interfaces.Catalogs;
using Soft_furniture.Service.Interfaces.Common;
using Soft_furniture.Service.Services.Catalogs;
using Soft_furniture.Service.Services.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ICatalogService, CatalogService>();
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
