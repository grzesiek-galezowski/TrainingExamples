using System;
using ConformingContainerAntipattern._3_ConformingContainer.Interfaces;
using ConformingContainerAntipattern._3_ConformingContainer.Services;

namespace ConformingContainerAntipattern._3_ConformingContainer.InMessages
{
  class StartMessage : AcmeMessage
  {
    public void AuthorizeUsing(IAuthorization authorizationRules)
    {
      Console.WriteLine("Authorizing Start with " + authorizationRules);
    }

    public void WriteTo(DataDestination dataDestination)
    {
      dataDestination.Add("Writing Start to " + dataDestination);
    }
  }
}