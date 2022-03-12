using Flurl.Http;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var urls = new Urls();
app.Configuration.Bind("Urls", urls);

app.MapPost("/broadcast", async ([FromBody] WorkDto workDto) =>
{
  await urls.Url1.PostJsonAsync(workDto);
  await urls.Url2.PostJsonAsync(workDto);
});

app.Run();

public record WorkDto;

public partial class Program
{
};