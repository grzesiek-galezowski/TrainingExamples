using FlowSimulation.ProductionCode;
using FluentAssertions;

namespace FlowSimulation.Specification;

public class ExpectedEvent(string text, Action<IEventsDestination> expectedCall)
{
  public readonly string Text = text;

  public void CheckAgainst(string actual) => Text.Should().Be(actual); //BUG use this

  public void CheckAgainst(IEventsDestination eventsDestination)
  {
    expectedCall.Invoke(eventsDestination);
  }
}