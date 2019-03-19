using ApplicationLogic.Ports;
using FluentAssertions;
using Lib;

namespace ComponentSpecification.AutomationLayer
{
  public class QueriedTestUser
  {
    private readonly UserDto _returnValue;

    public QueriedTestUser(UserDto returnValue)
    {
      _returnValue = returnValue;
    }

    public void ShouldBe(UserDtoBuilder johnny)
    {
      _returnValue.Should().BeEquivalentTo(johnny.Build());
    }
  }
}