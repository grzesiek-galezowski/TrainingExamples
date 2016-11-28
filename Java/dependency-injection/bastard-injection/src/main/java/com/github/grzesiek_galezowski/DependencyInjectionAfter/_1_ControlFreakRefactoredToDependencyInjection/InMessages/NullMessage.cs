using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Interfaces;
using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Services;

namespace DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.InMessages
{
  class NullMessage : AcmeMessage
  {
    public void AuthorizeUsing(IAuthorization authorizationRules)
    {
      
    }

    public void WriteTo(DataDestination dataDestination)
    {
       
    }
  }
}