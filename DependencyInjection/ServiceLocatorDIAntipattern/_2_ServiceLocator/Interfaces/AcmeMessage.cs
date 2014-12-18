using ServiceLocatorDIAntipattern._2_ServiceLocator.Services;

namespace ServiceLocatorDIAntipattern._2_ServiceLocator.Interfaces
{
  public interface AcmeMessage
  {
    void AuthorizeUsing(IAuthorization authorizationRules);
    void WriteTo(DataDestination dataDestination);
  }
}