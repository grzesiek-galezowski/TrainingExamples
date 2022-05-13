using ReturningResultsFromCommands;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();

app.MapPost("/todo1", () =>
{
    var service = new TodoServiceReturningData(new FakeApi(), new FakeValidation());
    var result = service.AddTodo(new Todo("Do the slides"));

    //ADVANTAGE: when dealing with the result, the logic is already done.
    //ADVANTAGE: we always get a return value and always one (this is how return statement works :-) )
    //DISADVANTAGE: when data is a request for a behavior, we need to interpret it to pick the right behavior
    //DISADVANTAGE: mixing side-effects (logging, telemetry?) and pure functions
    //              in the same piece of code (could be resolved via middleware?)
    return result.Match(
        success => Results.Ok(success.Id),
        failure =>
        {
            switch (failure.ErrorCode)
            {
                case ErrorCode.ApiCallFailed:
                {
                    logger.LogError($"Api call failed, {failure.Message}");
                    return Results.StatusCode(500);
                }
                case ErrorCode.ValidationFailed:
                {
                    logger.LogError($"Validation failed, {failure.Message}");
                    return Results.BadRequest();
                }
                default:
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        });
});

app.Run();