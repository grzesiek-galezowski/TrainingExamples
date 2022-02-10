namespace FrodoEntersTheRoom;

public class ResponseCommunicatingThroughHttp : IResponse
{
  private readonly HttpContext _httpContext;

  public ResponseCommunicatingThroughHttp(HttpContext httpContext)
  {
    _httpContext = httpContext;
  }

  public async Task Respond(string text, DialogState dialogState)
  {
    await Results.Ok($"[{dialogState}] " + text).ExecuteAsync(_httpContext);
  }
}