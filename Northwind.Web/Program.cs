using Packt.Shared;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddNorthwindContext();
builder.Services.AddRequestDecompression();
builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.ListenAnyIP(5001, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
        listenOptions.UseHttps(); // HTTP/3 requires secure connections
    });
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
    //Console.WriteLine("Htst are using.");
}

//app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();
app.UseRequestDecompression();

app.Use(async (HttpContext context, Func<Task> next) =>
{
    RouteEndpoint? rep = context.GetEndpoint() as RouteEndpoint;
    if (rep is not null)
    {
        Console.WriteLine($"Endpoint name: {rep.DisplayName}");
        Console.WriteLine($"Endpoint route pattern: {rep.RoutePattern.RawText}");
    }
    if (context.Request.Path == "/bonjour")
    {
        // in the case of a match on URL path, this becomes a terminating
        // delegate that returns so does not call the next delegate
        await context.Response.WriteAsync("Bonjour Monde!");
        return;
    }
    if (context.Request.Path == "/somepage")
    {
        // in the case of a match on URL path, this becomes a terminating
        // delegate that returns so does not call the next delegate
        await context.Response.WriteAsync("It is one more page!");
        return;
    }
    // we could modify the request before calling the next delegate
    await next();
    // we could modify the response after calling the next delegate
});

app.UseDefaultFiles(); // index.html, default.html, and so on
app.UseStaticFiles();

app.MapRazorPages();
app.MapGet("/hello", () => "Hello World!");

app.Run();

Console.WriteLine("This executes after the web server has stopped!");