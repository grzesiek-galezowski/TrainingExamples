using TelecomSystemNestedFunctions.Services;

namespace TelecomSystemNestedFunctions.Interfaces
{
  public interface AcmeMessage
  {
    void AuthorizeUsing(IAuthorization authorizationRules);
    void WriteTo(DataDestination dataDestination);
  }
}