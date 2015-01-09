using System;
using DependencyInjectionBefore._1_ControlFreak.Interfaces;
using DependencyInjectionBefore._1_ControlFreak.Services;

namespace DependencyInjectionBefore._1_ControlFreak.InMessages
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