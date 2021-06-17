using DependencyInjectionBefore._1_ControlFreak.Services;

namespace DependencyInjectionBefore._1_ControlFreak.Interfaces
{
  interface AcmeMessage
  {
    void AuthorizeUsing(ActiveDirectoryBasedAuthorization authorizationRules);
    void WriteTo(DataDestination dataDestination);
  }
}