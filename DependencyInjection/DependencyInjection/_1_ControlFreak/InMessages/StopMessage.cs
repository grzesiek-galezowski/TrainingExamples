using System;
using DependencyInjection._1_ControlFreak.Interfaces;
using DependencyInjection._1_ControlFreak.Services;

namespace DependencyInjection._1_ControlFreak.InMessages
{
  class StopMessage : AcmeMessage
  {
    public void AuthorizeUsing(ActiveDirectoryBasedAuthorization authorizationRules)
    {
      Console.WriteLine("Authorizing Stop with " + authorizationRules);
    }

    public void WriteTo(DataDestination dataDestination)
    {
      Console.WriteLine("Writing Stop to " + dataDestination);
    }
  }
}