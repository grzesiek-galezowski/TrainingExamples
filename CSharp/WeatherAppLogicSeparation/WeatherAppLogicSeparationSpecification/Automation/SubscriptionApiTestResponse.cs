using Application;
using FluentAssertions;
using Flurl.Http;

namespace WeatherAppLogicSeparationSpecification.Automation;

public class SubscriptionApiTestResponse(IFlurlResponse response)
{
  public async Task ShouldBeSubscriptionCreatedSuccessfully(Guid subscriptionId)
  {
    response.StatusCode.Should().Be(200);
    (await response.GetJsonAsync<SubscriptionCreatedDto>()).Should().Be(new SubscriptionCreatedDto(subscriptionId));
  }
}