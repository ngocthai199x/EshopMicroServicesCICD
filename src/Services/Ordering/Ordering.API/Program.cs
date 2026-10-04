using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);
//Add services to the container
//---------------------------------------------------------------
//Infrastructure
//Application services
//API - Carter, HealthCheck, ExceptionHandler
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();

//Configure the HTTP request pipeline.
app.UseApiServices();  
if(app.Environment.IsDevelopment())
{
    //The lasted database will be updated to the latest version when application start up
    await app.InitializeDatabaseAsync();
}
app.Run();
