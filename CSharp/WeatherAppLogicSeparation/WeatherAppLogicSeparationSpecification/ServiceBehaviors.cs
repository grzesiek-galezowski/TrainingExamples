using System.Collections.Immutable;
using Application;
using WeatherAppLogicSeparationSpecification.Automation;

namespace WeatherAppLogicSeparationSpecification;

public class ServiceBehaviors
{
  [Test]
  public async Task Whatever() //TODO improve in the future
  {
    var subscriptionId = Guid.NewGuid();
    var tenantId = Guid.NewGuid();
    await using var driver = new Driver();

    await driver.Start();

    var response = await driver.RequestSubscribingToWeatherNotifications(new SubscriptionRequestDto(
      tenantId,
      subscriptionId,
      Guid.NewGuid(),
      QueryTypes.DeviceIds,
      null,
      ImmutableList.Create("1", "2"),
      null));

    await response.ShouldBeSubscriptionCreatedSuccessfully(subscriptionId);
  }

  //bug unsubscribe
}