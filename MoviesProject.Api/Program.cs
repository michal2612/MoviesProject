using MoviesProject.Application.Services;
using MoviesProject.Application.Services.Impl;
using MoviesProject.Infrastructure.DAL;
using MoviesProject.Infrastructure.Exceptions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionMiddleware();
builder.Services.AddMoviesSqlLite(builder.Configuration.GetSection("ConnectionString").Value);
builder.Services.AddScoped<IMovieService, MovieService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
