using Microsoft.Extensions.Logging.Abstractions;
using ReturningResultsFromCommands;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();

app.MapPost("/todo1", () =>
{
    var service = new TodoServiceReturningData(new FakeApi(), new FakeValidation());
    var result = service.AddTodo(new Todo("Do the slides"));

    //ADVANTAGE: when dealing with the result, the logic is already done.
    //ADVANTAGE: we always get a return value and always one (this is how return statement works :-) )
    //DISADVANTAGE: when data is a request for a behavior, we need to interpret it to pick the right behavior
    //              - additional if statements
    //DISADVANTAGE: polluting request handler (can be somewhat resolved via factory etc.)
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


app.MapPost("/todo2", () =>
{
    //ADVANTAGE: when dealing with the result, the logic is already done.
    //ADVANTAGE: we always get a return value and always one (this is how return statement works :-) )
    //ADVANTAGE: no additional ifs (polymorphism instead of data interpretation, choice is pushed inside)
    //DISADVANTAGE: ToAspNetCoreResult couples the logic to asp.net core types
    //DISADVANTAGE: CQS violation inside the service
    //DISADVANTAGE: CQS violation inside the Responses (logging, potentially telemetry etc.)
    var service = new TodoServiceReturningPolymorphicObject(
        new FakeApi(), 
        new FakeValidation(), 
        new AspNetCoreResponseFactory(loggerFactory));

    var result = service.AddTodo(new Todo("Do the slides"));
    return result.ToAspNetCoreResult();
});

app.MapPost("/todo3", () =>
{
    var service = new TodoServiceAcceptingPolymorphicObject(
        new FakeApi(), 
        new FakeValidation());

    var responseInProgress = new AspNetCoreAddTodoResponseInProgress(
        loggerFactory.CreateLogger<AspNetCoreAddTodoResponseInProgress>());

    service.AddTodo(new Todo("Do the slides"), responseInProgress);
    return responseInProgress.ToAspNetCoreResult();
});

app.Run();