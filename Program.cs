var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Enable static files
app.UseStaticFiles();

app.MapGet("/", () => Results.Redirect("/index.html"));

app.Run();
