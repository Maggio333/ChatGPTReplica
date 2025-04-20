using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

app.MapPost("/", async (HttpContext context) =>
{
    using var reader = new StreamReader(context.Request.Body);
    var body = await reader.ReadToEndAsync();
    context.Response.ContentType = "application/json";

    if (body.Contains("podróżami"))
    {
        await context.Response.WriteAsync("""
        {"answer": "Zespół badawczy składa się z dr Jana Nowaka i mgr Anny Kowalskiej. Badania były prowadzone na Uniwersytecie Warszawskim, sponsor: Ministerstwo Nauki."}
        """);
    }
    else
    {
        await context.Response.WriteAsync("""
        {"answer": "Brak danych."}
        """);
    }
});

app.Run();
