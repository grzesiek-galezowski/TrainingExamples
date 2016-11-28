using ConformingContainerAntipattern._3_ConformingContainer.Interfaces;
using ConformingContainerAntipattern._3_ConformingContainer.Services;

namespace ConformingContainerAntipattern._3_ConformingContainer.InMessages
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