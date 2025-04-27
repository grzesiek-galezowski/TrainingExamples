using Core.Maybe;

namespace KataTrainReservationTddEbook.PersistentData;

public sealed record SeatDto(string Coach, int SeatNumber, Maybe<string> BookingReference);