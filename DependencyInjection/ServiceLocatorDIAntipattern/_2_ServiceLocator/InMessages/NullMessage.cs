using ServiceLocatorDIAntipattern._2_ServiceLocator.Interfaces;
using ServiceLocatorDIAntipattern._2_ServiceLocator.Services;

namespace ServiceLocatorDIAntipattern._2_ServiceLocator.InMessages
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