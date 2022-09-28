using Microsoft.Extensions.Options;
using TodoApp1.Bootstrap;
using TodoApp1.Database;
using TodoApp1.Endpoints;

namespace TodoApp1;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddSingleton(ctx => 
      new ServiceLogicRoot(
        ctx.GetRequiredService<IOptions<DatabaseOptions>>().Value));
    builder.Services.AddSingleton<IEndpointsRoot>(
      ctx => ctx.GetRequiredService<ServiceLogicRoot>());

    builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("Database"));

    var app = builder.Build();

    app.MapPost("/todo", async (HttpContext context) =>
    {
      await context.Endpoints().AddTodoEndpoint.Handle(context);
    });

    app.MapGet("/todo/{id}", async (HttpContext context) =>
    {
      await context.Endpoints().RetrieveTodoNoteEndpoint.Handle(context);
    });

    app.Run();
  }
}