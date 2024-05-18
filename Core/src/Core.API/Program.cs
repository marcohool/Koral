using Core.API.Configuration;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

/// <summary>
/// The <see cref="Program"/> class.
/// </summary>
public partial class Program { }
