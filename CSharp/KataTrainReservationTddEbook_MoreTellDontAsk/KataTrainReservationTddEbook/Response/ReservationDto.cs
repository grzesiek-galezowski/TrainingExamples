using LanguageExt;

namespace KataTrainReservationTddEbook.Response;

public sealed record ReservationDto(string TrainId,
  Seq<TicketDto> PerSeatTickets,
  string ReservationId);