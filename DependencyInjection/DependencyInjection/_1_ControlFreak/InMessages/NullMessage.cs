using DependencyInjection._1_ControlFreak.Interfaces;
using DependencyInjection._1_ControlFreak.Services;

namespace DependencyInjection._1_ControlFreak.InMessages
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