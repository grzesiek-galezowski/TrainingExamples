var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", (HttpRequest request, HttpResponse response) =>
{
  async IAsyncEnumerable<DateTime> GetStrings()
  {
    while(true)
    {
      yield return DateTime.Now;
      await Task.Delay(TimeSpan.FromSeconds(1));
      Console.WriteLine("Next loop");
    }
  }
  return GetStrings();
});

app.Run();
