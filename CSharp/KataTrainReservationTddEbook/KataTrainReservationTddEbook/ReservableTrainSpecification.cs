using System.Collections.Generic;
using Functional.Maybe;
using Functional.Maybe.Just;
using TddXt.XNSubstitute;

namespace KataTrainReservationTddEbook;

//There are various business rules and policies around which seats may be reserved.
//For a train overall, no more than 70% of seats may be reserved in advance,
//and ideally no individual coach should have no more than 70% reserved seats either.
//However, there is another business rule that says you must put all the seats for one
//reservation in the same coach.
//This could make you and go over 70% for some coaches,
//just make sure to keep to 70% for the whole train.

// scenario 1: 70% rule for train OK, fits any 100% OK, fits any 70% OK, 70% rule for first coach OK
// coach 1: 1A(v), 1B(x), 1C(x)
// coach 2: 2A(x), 2B(x), 2C(x)
// reservation: 1 place
// outcome:
// coach 1: 1A(v), 1B(v), 1C(x)
// coach 2: 2A(x), 2B(x), 2C(x)
//IN: train id, how many seats
//{"train_id": "express_2000", "booking_reference": "75bcd15", "seats": ["1B"]}

// scenario 2: 70% rule for train OK, fits any 100% OK, fits any 70% OK, 70% rule for first coach not OK
// coach 1: 1A(v), 1B(v), 1C(v), 1D(x)
// coach 2: 2A(x), 2B(x), 2C(x)
// reservation: 1 place
// outcome:
// coach 1: 1A(v), 1B(v), 1C(v), 1D(x)
// coach 2: 2A(v), 2B(x), 2C(x)
//IN: train id, how many seats
//{"train_id": "express_2000", "booking_reference": "75bcd15", "seats": ["2A"]}


// scenario 3: 70% rule for train OK, fits any 100% OK, fits any 70% NOT OK
// coach 1: 1A(v), 1B(v), 1C(v), 1D(x)
// coach 2: 2A(x), 2B(x), 2C(x)
// reservation: 1 place
// outcome:
// coach 1: 1A(v), 1B(v), 1C(v), 1D(x)
// coach 2: 2A(v), 2B(x), 2C(x)
//IN: train id, how many seats
//{"train_id": "express_2000", "booking_reference": "75bcd15", "seats": ["2A"]}

// scenario 4: 70% rule for train OK, fits any 100% not OK
// scenario 5: 100% rule for train not OK


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