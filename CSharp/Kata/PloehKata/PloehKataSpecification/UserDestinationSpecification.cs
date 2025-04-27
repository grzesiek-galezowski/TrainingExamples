using System;
using NSubstitute;
using PloehKata;
using PloehKata.Logic;
using PloehKata.Ports;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace PloehKataSpecification
{
  public class UserDestinationSpecification
  {
    [Fact]
    public void ShouldPersistReceivedUserDto()
    {
      //GIVEN
      var persistence = Substitute.For<IPersistence>();
      var userDestination = new UserDestination(persistence);
      var userDto = Any.Instance<UserDto>();

      //WHEN
      userDestination.Save(userDto);
      //THEN
      persistence.Received(1).Save("Users", userDto);
    }
  }
}