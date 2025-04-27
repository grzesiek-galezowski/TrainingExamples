using TodoApp1.Endpoints;

namespace TodoApp1;

internal static class DiContainerExtensions
{
  public static IEndpointsRoot Endpoints(this HttpContext context)
  {
    return context.RequestServices.GetRequiredService<IEndpointsRoot>();
  }
}