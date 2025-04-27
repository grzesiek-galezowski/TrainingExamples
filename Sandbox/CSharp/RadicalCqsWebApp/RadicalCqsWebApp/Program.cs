using Microsoft.Extensions.Logging.Abstractions;

namespace RadicalCqsWebApp;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddAuthorization();


    var app = builder.Build();

    // Configure the HTTP request pipeline.

    app.UseAuthorization();


    app.MapPost("/todo", (HttpContext httpContext) =>
    {
      var responseInProgress = new ResponseInProgress();
      new AddTodoItemCommand(
        responseInProgress,
        new AddTodoItemDto(
          "lol",
          "I laughed hard",
          "Zenek",
          DateTime.Now.Add(TimeSpan.FromDays(2))),
        new TranslationService(),
        new TodoItemsDao(),
        new ReminderApi(),
        NullLogger<AddTodoItemCommand>.Instance).Execute();
      return responseInProgress.ToResult();
    });

    app.Run();
  }
}