using System;
using BastardInjection._4_BastardInjection.Interfaces;
using BastardInjection._4_BastardInjection.Services;

namespace BastardInjection._4_BastardInjection.InMessages
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