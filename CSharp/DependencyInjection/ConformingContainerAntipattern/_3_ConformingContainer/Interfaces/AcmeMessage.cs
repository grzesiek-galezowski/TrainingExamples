using ConformingContainerAntipattern._3_ConformingContainer.Services;

namespace ConformingContainerAntipattern._3_ConformingContainer.Interfaces
{
  public interface AcmeMessage
  {
    void AuthorizeUsing(IAuthorization authorizationRules);
    void WriteTo(DataDestination dataDestination);
  }
}