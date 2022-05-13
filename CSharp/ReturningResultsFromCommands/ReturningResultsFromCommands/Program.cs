using ReturningResultsFromCommands;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapPost("/todo1", () =>
{
    var service = new TodoServiceReturningData(new FakeApi(), new FakeValidation());
    var result = service.AddTodo(new Todo("Do the slides"));

    return result.Match(
        success => Results.Ok(success.Id),
        failure => failure.ErrorCode switch
        {
            ErrorCode.ApiCallFailed => Results.StatusCode(500),
            ErrorCode.ValidationFailed => Results.BadRequest()
        });
});

app.Run();