using DependencyInjectionBefore._1_ControlFreak.Interfaces;
using DependencyInjectionBefore._1_ControlFreak.Services;

namespace DependencyInjectionBefore._1_ControlFreak.InMessages
{
  class NullMessage : AcmeMessage
  {
    public void AuthorizeUsing(ActiveDirectoryBasedAuthorization authorizationRules)
    {
      
    }

    public void WriteTo(DataDestination dataDestination)
    {
       
    }
  }
}