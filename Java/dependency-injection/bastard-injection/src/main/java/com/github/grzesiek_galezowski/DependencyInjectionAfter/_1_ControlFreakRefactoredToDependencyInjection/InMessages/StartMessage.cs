using System;
using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Interfaces;
using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Services;

namespace DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.InMessages
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