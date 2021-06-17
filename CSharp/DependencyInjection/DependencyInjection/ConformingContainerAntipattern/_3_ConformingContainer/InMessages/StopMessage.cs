using System;
using ConformingContainerAntipattern._3_ConformingContainer.Interfaces;
using ConformingContainerAntipattern._3_ConformingContainer.Services;

namespace ConformingContainerAntipattern._3_ConformingContainer.InMessages
{
  class StopMessage : AcmeMessage
  {
    public void AuthorizeUsing(IAuthorization authorizationRules)
    {
      Console.WriteLine("Authorizing Stop with " + authorizationRules);
    }

    public void WriteTo(DataDestination dataDestination)
    {
      Console.WriteLine("Writing Stop to " + dataDestination);
    }
  }
}