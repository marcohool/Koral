using Core.API;
using Core.Application;
using Core.DataAccess;
using Core.Domain;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwagger();
builder.Services.AddJwt(builder.Configuration);

builder
    .Services.AddDomainServices()
    .AddApplicationServices()
    .AddDataAccessServices(builder.Configuration);

WebApplication app = builder.Build();

app.Run();
