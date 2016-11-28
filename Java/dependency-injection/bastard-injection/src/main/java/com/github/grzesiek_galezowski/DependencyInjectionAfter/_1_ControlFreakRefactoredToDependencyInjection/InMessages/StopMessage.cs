using System;
using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Interfaces;
using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Services;

namespace DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.InMessages
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