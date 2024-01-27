using Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI
builder.Services
    .AddSingleton<INewsCalculator, NewsCalculator>(sp => 
        new NewsCalculator([MeasurementType.TEMP, MeasurementType.HR, MeasurementType.RR], sp)) // required measurements
    .AddKeyedSingleton<IMeasurementCalculator, MeasurementTempCalculator>(MeasurementType.TEMP)
    .AddKeyedSingleton<IMeasurementCalculator, MeasurementHRCalculator>(MeasurementType.HR)
    .AddKeyedSingleton<IMeasurementCalculator, MeasurementRRCalculator>(MeasurementType.RR);

builder.Services.Configure<RouteHandlerOptions>(options => options.ThrowOnBadRequest = true);

var app = builder.Build();

// Exception handling
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (errorFeature != null) 
        {
            var exception = errorFeature.Error;

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = Text.Plain;
            await context.Response.WriteAsync(exception.Message);
        }
    });
});

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