using Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI
builder.Services.AddSingleton<INewsCalculator, NewsCalculator>();
builder.Services.AddSingleton<IMeasurementCalculator, MeasurementCalculator>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Methods
app.MapPost("/calc-news", Results<Ok<NewsResult>, BadRequest<string>> (List<Measurement> measurements, [FromServices]INewsCalculator newsCalculator) => {
        try {
            return TypedResults.Ok(newsCalculator.Calculate(measurements));
        }
        catch (Exception e) {
            return TypedResults.BadRequest(e.Message);
        }
    })
    .WithName("CalculateNews")
    .WithOpenApi();

app.Run();