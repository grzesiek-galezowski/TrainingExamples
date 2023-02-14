﻿using KataTrainReservationTddEbook.Request;

namespace KataTrainReservationTddEbook;

public class TicketOfficeCommandFactory : ICommandFactory
{
  public IReservationCommand CreateReservationCommand(ReservationRequestDto requestDto,
    IReservationInProgress reservationInProgress)
  {
    return new TrainReservationCommand(
      new TrainId(requestDto.TrainId), 
      new ReferenceService(), 
      reservationInProgress,
      new StandardBookingProcess(new CheckThereAreEnoughSeatsInTheTrain(requestDto.SeatCount)));
  }
}

public class TicketOfficeCommandFactorySpecification
{
  [Fact]
  public void ShouldDowhat()
  {
    //GIVEN
    var factory = new TicketOfficeCommandFactory();
    var dto = Any.Instance<ReservationRequestDto>();
    var reservationInProgress = Any.Instance<IReservationInProgress>();

    //WHEN
    var command = factory.CreateReservationCommand(dto, reservationInProgress);

    //THEN
    command.Should().BeOfType<TrainReservationCommand>();
  }
}