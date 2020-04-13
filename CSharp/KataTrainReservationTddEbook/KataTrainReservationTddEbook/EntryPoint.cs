using System.Collections.Generic;
using System.Text;

namespace KataTrainReservationTddEbook
{
  public class EntryPoint
  {
    public void Main()
    {
      new WebApp(
        new TicketOffice(
          new TodoReservationInProgressFactory(), //TODO change the name
          new TicketOfficeCommandFactory())
      ).Host();
    }
  }
}
