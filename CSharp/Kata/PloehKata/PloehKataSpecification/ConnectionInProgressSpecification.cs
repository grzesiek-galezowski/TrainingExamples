using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PloehKata;
using PloehKata.Adapters;
using PloehKata.Ports;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace PloehKataSpecification
{
  public class ConnectionInProgressSpecification
  {
    [Fact]
    public void ShouldThrowNoResultExceptionWhenNoResultWasReported()
    {
      //GIVEN
      var connectionInProgress = new JsonBasedConnectionInProgress();

      //WHEN - THEN
      new Action(() => connectionInProgress.ToActionResult())
        .Should().ThrowExactly<NoResultException>();
    }

    [Fact]
    public void ShouldReturnNoUserFoundWhenNoUserFoundReported()
    {
      //GIVEN
      var connectionInProgress = new JsonBasedConnectionInProgress();
      connectionInProgress.UserNotFound();

      //WHEN
      var actionResult = connectionInProgress.ToActionResult();

      //THEN
      actionResult.Should().BeOfType<BadRequestObjectResult>()
        .Which.Value.Should().Be("User not found.");
    }
    
    [Fact]
    public void ShouldReturnNoOtherUserFoundWhenNoOtherUserFoundReported()
    {
      //GIVEN
      var connectionInProgress = new JsonBasedConnectionInProgress();
      connectionInProgress.OtherUserNotFound();

      //WHEN
      var actionResult = connectionInProgress.ToActionResult();

      //THEN
      actionResult.Should().BeOfType<BadRequestObjectResult>()
        .Which.Value.Should().Be("Other user not found.");
    }

    [Fact]
    public void ShouldReturnInvalidUserIdWhenInvalidUserIdReported()
    {
      //GIVEN
      var connectionInProgress = new JsonBasedConnectionInProgress();
      connectionInProgress.InvalidUserId();

      //WHEN
      var actionResult = connectionInProgress.ToActionResult();

      //THEN
      actionResult.Should().BeOfType<BadRequestObjectResult>()
        .Which.Value.Should().Be("Invalid user ID.");
    }

    [Fact]
    public void ShouldReturnInvalidOtherUserIdWhenInvalidOtherUserIdReported()
    {
      //GIVEN
      var connectionInProgress = new JsonBasedConnectionInProgress();
      connectionInProgress.InvalidOtherUserId();

      //WHEN
      var actionResult = connectionInProgress.ToActionResult();

      //THEN
      actionResult.Should().BeOfType<BadRequestObjectResult>()
        .Which.Value.Should().Be("Invalid other user ID.");
    }
    
    [Fact]
    public void ShouldReturnUserConvertedToJsonWhenSuccessReported()
    {
      //GIVEN
      var connectionInProgress = new JsonBasedConnectionInProgress();
      var userDto = Any.Instance<UserDto>();
      connectionInProgress.Success(userDto);

      //WHEN
      var actionResult = connectionInProgress.ToActionResult();

      //THEN
      actionResult.Should().BeOfType<JsonResult>()
        .Which.Value.Should().Be(userDto);
    }

  }
}