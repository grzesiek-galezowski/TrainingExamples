using System;
using ServiceLocatorDIAntipattern._2_ServiceLocator.Interfaces;
using ServiceLocatorDIAntipattern._2_ServiceLocator.Services;

namespace ServiceLocatorDIAntipattern._2_ServiceLocator.InMessages
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