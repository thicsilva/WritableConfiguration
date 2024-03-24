using Microsoft.AspNetCore.Mvc;
using WritableOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Configuration.AddWritableJsonFile("appsettings.json", true, true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(typeof(ConfigurationsService<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/configs",
    (ConfigurationsService<ServiceConfig> options) =>
        Results.Ok((object?)options.GetFileConfiguration("ServiceConfig")));
app.MapPost("/configs", (ConfigurationsService<ServiceConfig> options, [FromBody] ServiceConfig serviceConfig) =>
{
    options.SetFileConfiguration("ServiceConfig", serviceConfig);
    return Results.NoContent();
});

app.Run();