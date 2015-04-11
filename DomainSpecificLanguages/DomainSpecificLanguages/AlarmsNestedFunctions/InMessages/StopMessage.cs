using System;
using TelecomSystemNestedFunctions.Interfaces;
using TelecomSystemNestedFunctions.Services;

namespace TelecomSystemNestedFunctions.InMessages
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