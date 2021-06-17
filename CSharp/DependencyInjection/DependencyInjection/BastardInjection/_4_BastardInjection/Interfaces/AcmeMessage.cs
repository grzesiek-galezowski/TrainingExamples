using BastardInjection._4_BastardInjection.Services;

namespace BastardInjection._4_BastardInjection.Interfaces
{
  public interface AcmeMessage
  {
    void AuthorizeUsing(IAuthorization authorizationRules);
    void WriteTo(DataDestination dataDestination);
  }
}