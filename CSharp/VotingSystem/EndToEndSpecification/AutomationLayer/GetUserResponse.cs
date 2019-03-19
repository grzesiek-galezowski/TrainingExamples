using System.Net;
using ApplicationLogic.Ports;
using FluentAssertions;
using Lib;

namespace EndToEndSpecification.AutomationLayer
{
  public class GetUserResponse
  {
    private readonly UserDto _userDto;
    private readonly HttpStatusCode _resultStatusCode;

    public GetUserResponse(UserDto userDto, HttpStatusCode resultStatusCode)
    {
      _userDto = userDto;
      _resultStatusCode = resultStatusCode;
    }

    public void ShouldIndicateSuccess()
    {
      _resultStatusCode.Should().Be(HttpStatusCode.OK);
    }

    public void ShouldBe(UserDtoBuilder johnny)
    {
      _userDto.Should().BeEquivalentTo(johnny.Build());
    }
  }
}