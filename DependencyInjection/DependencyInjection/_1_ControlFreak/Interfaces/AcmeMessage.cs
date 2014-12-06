using DependencyInjection._1_ControlFreak.Services;

namespace DependencyInjection._1_ControlFreak.Interfaces
{
  interface AcmeMessage
  {
    void AuthorizeUsing(ActiveDirectoryBasedAuthorization authorizationRules);
    void WriteTo(DataDestination dataDestination);
  }
}