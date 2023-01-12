using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SantaShop.Common.Options;
using SantaShop.Db;
using SantaShop.Infrastructure;
using SantaShop.Modules;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<ServiceOptions>(builder.Configuration);
builder.Services.Configure<DatabaseOptions>(builder.Configuration);

var sp = builder.Services.BuildServiceProvider();
var dbOptions = sp.GetService<IOptions<DatabaseOptions>>();
builder.Services.RegisterGiftShopContext(dbOptions);


builder.Services.AddControllers();
builder.Services.AddMvc();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger(sp);

var assembly = typeof(IInfrastructureMarker).Assembly;
builder.Services.AddAutoMapper(assembly);
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddDbServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
var service_options = app.Services.GetService<IOptions<ServiceOptions>>();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("swagger/v1/swagger.json", service_options!.Value.Service_Description);
       
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//DB Migration
var scope = app.Services.CreateScope();
var factory = scope.ServiceProvider.GetService(typeof(IDbContextFactory<GiftShopContext>)) as IDbContextFactory<GiftShopContext>;
if (factory != null)
{
    using (var dbContext = factory.CreateDbContext())
    {
        dbContext?.Database.Migrate();
    }
}

app.Run();

public partial class Program { }