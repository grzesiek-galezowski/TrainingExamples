using System;
using System.Collections.Generic;
using System.Text;

namespace KataTrainReservationTddEbook.PersistentData
{
  public class Seat
  {
    public string Coach { get; private set; }
    public int SeatNumber { get; private set; }

    public Seat(string coach, int seatNumber)
    {
      this.Coach = coach;
      this.SeatNumber = seatNumber;
    }

    /// <summary>
    /// N.B. this is not how you would override equals in a production environment. :)
    /// </summary>
    public override bool Equals(object obj)
    {
      Seat other = obj as Seat;

      return this.Coach == other.Coach && this.SeatNumber == other.SeatNumber;
    }
  }
}
