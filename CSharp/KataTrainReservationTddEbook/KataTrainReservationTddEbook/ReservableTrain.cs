using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Functional.Maybe;
using Functional.Maybe.Just;
using NSubstitute;
using TddXt.AnyRoot.Collections;
using TddXt.AnyRoot.Numbers;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace KataTrainReservationTddEbook;

public class ReservableTrain : ITrain
{
  private readonly IEnumerable<ICoach> _coaches;

  public ReservableTrain(IEnumerable<ICoach> coaches)
  {
    _coaches = coaches;
  }

  public const uint UpperBound = 10; //bug

  public void Reserve(in uint seatCount, ISearchEngine searchEngine, IReservationInProgress reservationInProgress)
  {
    var chosenCoach = searchEngine.FindCoachForReservation(_coaches, seatCount);
    if (chosenCoach.HasValue)
    {
      chosenCoach.Value.Reserve(seatCount, reservationInProgress);
    }
    else
    {
      reservationInProgress.NoMatchingCoachFoundFor(seatCount);
    }
  }

  public bool HasCapacityForReservationsInAdvance()
  {
    var percentages = _coaches.Select(c => c.GetPercentageReserved());
    if (percentages.Average(p => p) <= UpperBound)
    {
      return true;
    }
    return false;
  }
}

public class ReservableTrainSpecification
{
  [Fact]
  public void ShouldDowhat()
  {
    //GIVEN
    var coaches = Any.Enumerable<ICoach>();
    var train = new ReservableTrain(coaches);
    var seatCount = Any.UnsignedInt();
    var searchEngine = Substitute.For<ISearchEngine>();
    var reservationInProgress = Any.Instance<IReservationInProgress>();
    var matchingCoach = Substitute.For<ICoach>();

    searchEngine.FindCoachForReservation(coaches, seatCount).Returns(matchingCoach.Just());

    //WHEN
    train.Reserve(seatCount, searchEngine, reservationInProgress);

    //THEN
    matchingCoach.Received(1).Reserve(seatCount, reservationInProgress); //bug XReceived.Only()
  }
    
  [Fact]
  public void ShouldDowhat2()
  {
    //GIVEN
    var coaches = Any.Enumerable<ICoach>();
    var train = new ReservableTrain(coaches);
    var seatCount = Any.UnsignedInt();
    var searchEngine = Substitute.For<ISearchEngine>();
    var reservationInProgress = Substitute.For<IReservationInProgress>();

    searchEngine.FindCoachForReservation(coaches, seatCount).Returns(Maybe<ICoach>.Nothing);

    //WHEN
    train.Reserve(seatCount, searchEngine, reservationInProgress);

    //THEN
    reservationInProgress.Received(1).NoMatchingCoachFoundFor(seatCount); //bug XReceived.Only()
  }
    
  [Theory]
  [InlineData(ReservableTrain.UpperBound, ReservableTrain.UpperBound+1, ReservableTrain.UpperBound, false)]
  [InlineData(ReservableTrain.UpperBound, ReservableTrain.UpperBound, ReservableTrain.UpperBound, true)]
  [InlineData(ReservableTrain.UpperBound-1, ReservableTrain.UpperBound+1, ReservableTrain.UpperBound, true)]
  public void ShouldDecideWhetherItHasCapacityBasedOnCapacityBeingOverTheUpperBound(
    uint percentage1,
    uint percentage2,
    uint percentage3,
    bool expectedResult)
  {
    //GIVEN
    var coach1 = Substitute.For<ICoach>();
    var coach2 = Substitute.For<ICoach>();
    var coach3 = Substitute.For<ICoach>();
    var coaches = new List<ICoach>
    {
      coach1,
      coach2,
      coach3,
    };
    var train = new ReservableTrain(coaches);

    coach1.GetPercentageReserved().Returns(percentage1);
    coach2.GetPercentageReserved().Returns(percentage2);
    coach3.GetPercentageReserved().Returns(percentage3);
    //bug value objects

    //WHEN
    var result = train.HasCapacityForReservationsInAdvance();

    //THEN
    result.Should().Be(expectedResult);
  }
}