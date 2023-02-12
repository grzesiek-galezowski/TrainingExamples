using System.Collections.Generic;

namespace KataTrainReservationTddEbook;

public class ReservationDto
{
  public readonly string trainId;
  public readonly string reservationId;
  public readonly List<TicketDto> perSeatTickets;

  public ReservationDto(
    string trainId,
    List<TicketDto> perSeatTickets,
    string reservationId)
  {
    this.trainId = trainId;
    this.perSeatTickets = perSeatTickets;
    this.reservationId = reservationId;
  }
}