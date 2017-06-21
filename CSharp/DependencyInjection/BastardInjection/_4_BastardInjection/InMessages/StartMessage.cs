using System;
using BastardInjection._4_BastardInjection.Interfaces;
using BastardInjection._4_BastardInjection.Services;

namespace BastardInjection._4_BastardInjection.InMessages
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