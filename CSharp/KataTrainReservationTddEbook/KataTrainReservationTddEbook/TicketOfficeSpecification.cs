using NSubstitute;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace KataTrainReservationTddEbook;

public class TicketOfficeSpecification
{
  [Fact] public void
    ShouldExecuteReservationCommandAndReturnResponseWhenMakingReservation()
  {
    //GIVEN
    var requestDto = Any.Instance<ReservationRequestDto>();
    var commandFactory = Substitute.For<CommandFactory>();
    var reservationInProgressFactory = Substitute.For<ReservationInProgressFactory>();
    var reservationInProgress = Substitute.For<ReservationInProgress>();
    var expectedReservationDto = Any.Instance<ReservationDto>();
    var reservationCommand = Substitute.For<ReservationCommand>();

    var ticketOffice = new TicketOffice(
      reservationInProgressFactory,
      commandFactory);

    reservationInProgressFactory.FreshInstance().Returns(reservationInProgress);
    commandFactory.CreateReservationCommand(requestDto, reservationInProgress).Returns(reservationCommand);
    reservationInProgress.ToDto().Returns(expectedReservationDto);

    //WHEN
    var reservationDto = ticketOffice.MakeReservation(requestDto);

    //THEN
    Assert.Equal(expectedReservationDto, reservationDto);
    reservationCommand.Received(1).Execute();
  }

}