using Core.Application;
using Core.DataAccess;
using Core.Domain;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

builder
    .Services.AddDomainServices()
    .AddApplicationServices()
    .AddDataAccessServices(builder.Configuration);

app.Run();
