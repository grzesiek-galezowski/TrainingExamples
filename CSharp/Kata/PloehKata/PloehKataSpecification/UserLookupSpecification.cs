using System;
using FluentAssertions;
using Functional.Maybe;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using PloehKata.Logic;
using PloehKata.Ports;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using TddXt.XFluentAssert.Root;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace PloehKataSpecification
{
  public class UserLookupSpecification
  {
    [Fact]
    public void ShouldCreateConnectorWithReadUser()
    {
      //GIVEN
      var persistence = Substitute.For<IPersistence>();
      var lookup = new UserLookup(persistence);
      var userDto = Any.Instance<UserDto>().ToMaybe();
      var id = Any.String();

      persistence.ReadById<UserDto>("Users", id).Returns(userDto);

      //WHEN
      var connector = lookup.LookupConnector(id);

      //THEN
      connector.Should().BeOfType<Connector>()
        .And.DependOn(userDto.Value);
    }
    
    [Fact]
    public void ShouldCreateNoConnectorWhenUserWithGivenIdDoesNotExist()
    {
      //GIVEN
      var persistence = Substitute.For<IPersistence>();
      var lookup = new UserLookup(persistence);
      var id = Any.String();

      persistence.ReadById<UserDto>("Users", id).Returns(Maybe<UserDto>.Nothing);

      //WHEN
      var connector = lookup.LookupConnector(id);

      //THEN
      connector.Should().BeOfType<NoConnector>();
    }
    [Fact]
    public void ShouldThrowInvalidConnectorIdExceptionWhenReadingConnectorRaisesException()
    {
      //GIVEN
      var persistence = Substitute.For<IPersistence>();
      var lookup = new UserLookup(persistence);
      var persistenceException = Any.Exception();
      var id = Any.String();

      persistence.ReadById<UserDto>("Users", id).Throws(persistenceException);

      //WHEN - THEN
      new Action(() => lookup.LookupConnector(id)).Should().Throw<InvalidConnectorIdException>()
        .Where(e => e.InnerException == persistenceException)
        .Where(e => e.Message.Contains(id));
    }
    
    [Fact]
    public void ShouldCreateConnecteeWithReadUser()
    {
      //GIVEN
      var persistence = Substitute.For<IPersistence>();
      var lookup = new UserLookup(persistence);
      var userDto = Any.Instance<UserDto>().ToMaybe();
      var id = Any.String();

      persistence.ReadById<UserDto>("Users", id).Returns(userDto);

      //WHEN
      var connector = lookup.LookupConnectee(id);

      //THEN
      connector.Should().BeOfType<Connectee>()
        .And.DependOn(userDto.Value);
    }
    
    [Fact]
    public void ShouldCreateNoConnecteeWhenUserWithGivenIdDoesNotExist()
    {
      //GIVEN
      var persistence = Substitute.For<IPersistence>();
      var lookup = new UserLookup(persistence);
      var id = Any.String();

      persistence.ReadById<UserDto>("Users", id).Returns(Maybe<UserDto>.Nothing);

      //WHEN
      var connectee = lookup.LookupConnectee(id);

      //THEN
      connectee.Should().BeOfType<NoConnectee>();
    }

    [Fact]
    public void ShouldThrowInvalidConnecteeIdExceptionWhenReadingConnecteeRaisesException()
    {
      //GIVEN
      var persistence = Substitute.For<IPersistence>();
      var lookup = new UserLookup(persistence);
      var persistenceException = Any.Exception();
      var id = Any.String();

      persistence.ReadById<UserDto>("Users", id).Throws(persistenceException);

      //WHEN - THEN
      new Action(() => lookup.LookupConnectee(id)).Should().Throw<InvalidConnecteeIdException>()
        .Where(e => e.InnerException == persistenceException)
        .Where(e => e.Message.Contains(id));
    }

  }
}