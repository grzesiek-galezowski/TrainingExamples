using FluentAssertions;
using TddXt.AnyRoot;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace KataTrainReservationTddEbook
{
  public class TicketOfficeCommandFactory : CommandFactory
  {
    public ReservationCommand CreateReservationCommand(ReservationRequestDto requestDto,
      ReservationInProgress reservationInProgress)
    {
      return new TrainReservationCommand(requestDto.seatCount, reservationInProgress, new ReservableTrain(null));
    }
  }

  public class TicketOfficeCommandFactorySpecification
  {
    [Fact]
    public void ShouldDOWHAT()
    {
      //GIVEN
      var factory = new TicketOfficeCommandFactory();
      var dto = Any.Instance<ReservationRequestDto>();
      var reservationInProgress = Any.Instance<ReservationInProgress>();

      //WHEN
      var command = factory.CreateReservationCommand(dto, reservationInProgress);

      //THEN
      command.Should().BeOfType<TrainReservationCommand>();
    }
  }
}