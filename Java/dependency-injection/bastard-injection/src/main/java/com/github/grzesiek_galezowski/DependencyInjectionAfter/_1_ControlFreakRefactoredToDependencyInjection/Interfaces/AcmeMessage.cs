using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Services;

namespace DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Interfaces
{
  public interface AcmeMessage
  {
    void AuthorizeUsing(IAuthorization authorizationRules);
    void WriteTo(DataDestination dataDestination);
  }
}