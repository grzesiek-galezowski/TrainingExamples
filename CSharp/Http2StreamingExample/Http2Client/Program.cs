using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://localhost:7097/");
httpClient.DefaultRequestVersion = new Version(2, 0); //streaming works on 1.1 as well

var responseStream = await httpClient.GetStreamAsync("/");

var enumerable = JsonSerializer.DeserializeAsyncEnumerable<DateTime>(
  responseStream, new JsonSerializerOptions(JsonSerializerDefaults.Web)
{
  DefaultBufferSize = 32
});

await foreach (var item in enumerable)
{
  Console.WriteLine(item);
}
