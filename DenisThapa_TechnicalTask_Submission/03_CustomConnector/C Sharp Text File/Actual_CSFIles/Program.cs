using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Bind to Render's PORT (defaults to 10000 if not set)
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Urls.Add($"http://0.0.0.0:{port}");

// POST /clean  { "phone": "(07) 3456-7890" } -> { "cleanedPhone": "0734567890" }
app.MapPost("/clean", (PhonePayload payload) =>
{
    var digits = Regex.Replace(payload?.phone ?? "", "[^0-9]", "");
    return Results.Json(new { cleanedPhone = digits });
});

app.Run();

record PhonePayload(string? phone);