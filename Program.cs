/*var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Enable static files
app.UseStaticFiles();

app.MapGet("/", () => Results.Redirect("/index.html"));

app.Run();*/

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Video}/{action=AddTimestamp}/{id?}");

app.Run();

