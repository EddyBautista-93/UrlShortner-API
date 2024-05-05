var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var urlMap = new Dictionary<string, string>();

app.MapGet("/", () => "Hello World!");

app.MapPost("/shorten",(HttpContext http, Dictionary<string,string> urlMap) =>
{
    string originalUrl = http.Request.Query["url"]; 
    // generate a short key
    string key = Guid.NewGuid().ToString().Substring(0, 8);
    if(urlMap.ContainsKey(key))
    {
        urlMap[key] = originalUrl;
        return Results.Ok($"Shortened URL: {http.Request.Scheme}://{http.Request.Host}/{key}");

    }
    return Results.Conflict("try again, duplicate key generated");sd

});

app.Run();
