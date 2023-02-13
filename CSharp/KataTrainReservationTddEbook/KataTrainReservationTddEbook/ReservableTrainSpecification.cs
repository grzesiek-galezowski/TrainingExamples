using System.Collections.Generic;
using Functional.Maybe;
using Functional.Maybe.Just;
using TddXt.XNSubstitute;

namespace KataTrainReservationTddEbook;

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
    matchingCoach.ReceivedOnly(1).Reserve(seatCount, reservationInProgress);
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
    reservationInProgress.ReceivedOnly(1).NoMatchingCoachFoundFor(seatCount);
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