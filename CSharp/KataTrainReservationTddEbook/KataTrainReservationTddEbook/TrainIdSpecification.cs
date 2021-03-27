using System;
using AutoFixture;
using AutoFixture.Idioms;
using FluentAssertions;
using TddXt.XFluentAssert.Api;
using Xunit;

namespace KataTrainReservationTddEbook
{
  public class TrainIdSpecification
  {
    [Fact]
    public void ShouldImplementEqualityCorrectly()
    {
      typeof(TrainId).Should().HaveValueSemantics();
    }
  }
}