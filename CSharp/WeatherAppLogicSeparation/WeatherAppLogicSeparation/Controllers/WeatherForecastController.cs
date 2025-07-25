using System.Text.Json;
using Application;
using Core.Either;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace WeatherAppLogicSeparation.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ApplicationLogic applicationLogic, IMetrics metrics, ILogger logger)
  : ControllerBase, ISubscribeCommandResponse
{
  [HttpPost(Name = "SubscribeToWeatherForecast")]
  public async Task Subscribe(CancellationToken token)
  {
    var requestDto = await ParseRequest();

    await requestDto.Match(
      async error => await BadRequest(new { Message = error }).ExecuteResultAsync(ControllerContext),
      async dto => await applicationLogic.CreateSubscribeCommand(dto, this).Execute(token));
  }

  async Task ISubscribeCommandResponse.NoDevicesForQuery(Guid subscriptionId)
  {
    logger.Warning("No devices found");
    await NotFound(new
    {
      SubscriptionId = subscriptionId
    }).ExecuteResultAsync(ControllerContext);
  }

  async Task ISubscribeCommandResponse.SubscriptionAlreadyExists(
    Guid subscriptionId, SubscriptionAlreadyExistsException exception)
  {
    logger.Warning(exception, "Subscription with this id already exists");
    await Conflict(
        new { SubscriptionId = subscriptionId, Message = exception.Message })
      .ExecuteResultAsync(ControllerContext);
  }

  async Task ISubscribeCommandResponse.SubscriptionCreated(Guid subscriptionId)
  {
    logger.Information("Subscription created successfully");
    await Ok(new SubscriptionCreatedDto(subscriptionId)).ExecuteResultAsync(ControllerContext);
  }

  async Task ISubscribeCommandResponse.ResolutionSubjectNotFound(Guid subscriptionId)
  {
    logger.Warning("Subscription with this id already exists");
    await BadRequest(new
    {
      SubscriptionId = subscriptionId
    }).ExecuteResultAsync(ControllerContext);
  }

  public async Task ErrorWhileResolvingDevices(BadResolutionException exception)
  {
    logger.Error(exception, "Could not resolve the query due to errors");
    await metrics.ReportException(exception);
    await Problem(exception.Message, statusCode: 502).ExecuteResultAsync(ControllerContext);
  }

  public async Task UnexpectedError(Exception exception)
  {
    logger.Error(exception, "Unexpected error occurred");
    await metrics.ReportException(exception);
    await Problem("Unexpected error occurred", statusCode: 500).ExecuteResultAsync(ControllerContext);
  }

  private async Task<Either<string, SubscriptionRequestDto>> ParseRequest()
  {
    var subscriptionRequestDto =
      await JsonSerializer.DeserializeAsync<SubscriptionRequestDto>(HttpContext.Request.Body);

    // TODO: more "syntactic" validations here or delegate to another object

    if (subscriptionRequestDto is null)
    {
      return "Could not parse request";
    }
    return subscriptionRequestDto;
  }
}