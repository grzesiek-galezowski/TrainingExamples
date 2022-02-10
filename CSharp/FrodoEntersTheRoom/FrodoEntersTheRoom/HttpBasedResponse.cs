namespace FrodoEntersTheRoom;

public class HttpBasedResponse : IResponse
{
  private readonly HttpContext _httpContext;

  public HttpBasedResponse(HttpContext httpContext)
  {
    _httpContext = httpContext;
  }

  public async Task Respond(string text, DialogState dialogState)
  {
    await Results.Ok($"[{dialogState}] " + text).ExecuteAsync(_httpContext);
  }
}