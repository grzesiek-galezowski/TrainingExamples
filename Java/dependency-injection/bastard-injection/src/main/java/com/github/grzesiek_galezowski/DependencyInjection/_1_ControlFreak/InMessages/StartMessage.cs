using System;
using DependencyInjectionBefore._1_ControlFreak.Interfaces;
using DependencyInjectionBefore._1_ControlFreak.Services;

namespace DependencyInjectionBefore._1_ControlFreak.InMessages
{
  class StartMessage : AcmeMessage
  {
    public void AuthorizeUsing(ActiveDirectoryBasedAuthorization authorizationRules)
    {
      Console.WriteLine("Authorizing Start with " + authorizationRules);
    }

    public void WriteTo(DataDestination dataDestination)
    {
      dataDestination.Add("Writing Start to " + dataDestination);
    }
  }
}