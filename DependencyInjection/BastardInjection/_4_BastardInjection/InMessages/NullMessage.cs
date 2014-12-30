using BastardInjection._4_BastardInjection.Interfaces;
using BastardInjection._4_BastardInjection.Services;

namespace BastardInjection._4_BastardInjection.InMessages
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