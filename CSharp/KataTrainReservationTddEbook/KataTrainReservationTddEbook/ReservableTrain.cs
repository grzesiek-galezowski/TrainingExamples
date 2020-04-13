using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Functional.Maybe;
using Functional.Maybe.Just;
using NSubstitute;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Collections;
using TddXt.AnyRoot.Numbers;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace KataTrainReservationTddEbook
{
  public class ReservableTrain : Train
  {
    private readonly IEnumerable<Coach> _coaches;

    public ReservableTrain(IEnumerable<Coach> coaches)
    {
      _coaches = coaches;
    }

    public void Reserve(in uint seatCount, SearchEngine searchEngine, ReservationInProgress reservationInProgress)
    {
      var chosenCoach = searchEngine.FindCoachForReservation(_coaches, seatCount);
      chosenCoach.Value.Reserve(seatCount, reservationInProgress);
    }
  }

  public class ReservableTrainSpecification
  {
    [Fact]
    public void ShouldDOWHAT()
    {
      //GIVEN
      var coaches = Any.Enumerable<Coach>();
      var train = new ReservableTrain(coaches);
      var seatCount = Any.UnsignedInt();
      var searchEngine = Substitute.For<SearchEngine>();
      var reservationInProgress = Any.Instance<ReservationInProgress>();
      var matchingCoach = Substitute.For<Coach>();

      searchEngine.FindCoachForReservation(coaches, seatCount).Returns(matchingCoach.Just());

      //WHEN
      train.Reserve(seatCount, searchEngine, reservationInProgress);

      //THEN
      matchingCoach.Received(1).Reserve(seatCount, reservationInProgress);
    }
    
    [Fact]
    public void ShouldDOWHAT2()
    {
      //GIVEN
      var coaches = Any.Enumerable<Coach>();
      var train = new ReservableTrain(coaches);
      var seatCount = Any.UnsignedInt();
      var searchEngine = Substitute.For<SearchEngine>();
      var reservationInProgress = Any.Instance<ReservationInProgress>();
      var matchingCoach = Substitute.For<Coach>();

      searchEngine.FindCoachForReservation(coaches, seatCount).Returns(Maybe<Coach>.Nothing);

      //WHEN
      train.Reserve(seatCount, searchEngine, reservationInProgress);

      //THEN
      reservationInProgress.Received(1).NoMatchingCoachFoundFor(seatCount);
    }
  }
}