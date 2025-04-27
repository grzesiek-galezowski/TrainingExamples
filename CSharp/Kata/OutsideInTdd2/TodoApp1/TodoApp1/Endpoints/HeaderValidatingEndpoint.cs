namespace TodoApp1.Endpoints;

public class HeaderValidatingEndpoint : IEndpoint
{
  private readonly IEndpoint _next;
  private readonly string _headerName;
  private readonly string _expectedValue;

  public HeaderValidatingEndpoint(string headerName,
    string expectedValue, IEndpoint next)
  {
    _next = next;
    _headerName = headerName;
    _expectedValue = expectedValue;
  }

  public async Task Handle(HttpContext context)
  {
    if (!context.Request.Headers[_headerName].Any())
    {
      await Results.BadRequest().ExecuteAsync(context);
    }
    else if (context.Request.Headers[_headerName].First() != _expectedValue)
      //bug test with multiple header values??
    {
      await Results.BadRequest().ExecuteAsync(context);
    }
    else
    {
      await _next.Handle(context);
    }
  }
}