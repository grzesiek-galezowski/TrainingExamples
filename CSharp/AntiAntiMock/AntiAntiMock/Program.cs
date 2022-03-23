using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using MockNoMock;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var urls = new Urls();
app.Configuration.Bind("Urls", urls);

app.MapPost("/broadcast", async ([FromBody] Work workDto) =>
{
    await urls.Url1.PostJsonAsync(workDto);
    await urls.Url2.PostJsonAsync(workDto);
});

app.Run();

public partial class Program
{
}
