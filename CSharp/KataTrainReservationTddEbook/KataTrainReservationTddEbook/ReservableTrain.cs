using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
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

    public bool HasRoomInPreferredCoachFor(uint seatCount)
    {
      throw new System.NotImplementedException();
    }

    public void ReserveSeatsInPreferredCoach(uint seatCount, ReservationInProgress reservationInProgress)
    {
      throw new System.NotImplementedException();
    }

    public bool HasRoomInAnyCoachFor(uint seatCount)
    {
      return _coaches.Any(c => c.HasRoomFor(seatCount));
    }

    public void ReserveSeatsInAnyFreeCoach(uint seatCount, ReservationInProgress reservationInProgress)
    {
      throw new System.NotImplementedException();
    }

    public bool HasPreferredRoomFor(uint seatCount)
    {
      return _coaches.Any(c => c.HasPreferredRoomFor(seatCount));
    }
  }

  public class ReservableTrainSpecification
  {
    [Fact]
    public void ShouldDOWHAT()
    {
      //GIVEN
      var coach1 = Substitute.For<Coach>();
      var coach2 = Substitute.For<Coach>();
      var coach3 = Substitute.For<Coach>();
      var seatCount = Any.UnsignedInt();
      var coaches = new List<Coach>
      {
         coach1,
         coach2,
         coach3,
      };

      coach1.HasPreferredRoomFor(seatCount).Returns(false);
      coach2.HasPreferredRoomFor(seatCount).Returns(true);
      coach3.HasPreferredRoomFor(seatCount).Returns(false);

      var reservableTrain = new ReservableTrain(coaches);

      //WHEN
      var hasEnoughRoom = reservableTrain.HasPreferredRoomFor(seatCount);

      //THEN
      hasEnoughRoom.Should().BeTrue();
    }
    
    [Fact]
    public void ShouldDOWHAT2()
    {
      //GIVEN
      var coach1 = Substitute.For<Coach>();
      var coach2 = Substitute.For<Coach>();
      var coach3 = Substitute.For<Coach>();
      var seatCount = Any.UnsignedInt();
      var coaches = new List<Coach>
      {
         coach1,
         coach2,
         coach3,
      };

      coach1.HasPreferredRoomFor(seatCount).Returns(false);
      coach2.HasPreferredRoomFor(seatCount).Returns(false);
      coach3.HasPreferredRoomFor(seatCount).Returns(false);

      var reservableTrain = new ReservableTrain(coaches);

      //WHEN
      var hasEnoughRoom = reservableTrain.HasPreferredRoomFor(seatCount);

      //THEN
      hasEnoughRoom.Should().BeFalse();
    }

    [Fact]
    public void ShouldDOWHAT3()
    {
      //GIVEN
      var coach1 = Substitute.For<Coach>();
      var coach2 = Substitute.For<Coach>();
      var coach3 = Substitute.For<Coach>();
      var seatCount = Any.UnsignedInt();
      var coaches = new List<Coach>
      {
         coach1,
         coach2,
         coach3,
      };

      coach1.HasRoomFor(seatCount).Returns(false);
      coach2.HasRoomFor(seatCount).Returns(false);
      coach3.HasRoomFor(seatCount).Returns(false);

      var reservableTrain = new ReservableTrain(coaches);

      //WHEN
      var hasEnoughRoom = reservableTrain.HasRoomInAnyCoachFor(seatCount);

      //THEN
      hasEnoughRoom.Should().BeFalse();
    }
  }
}