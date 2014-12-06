using Validations.ThirdParty;

namespace Validations
{
  public class RequestProcessing
  {
    private readonly RequestValidation _basicRequestValidation;

    public RequestProcessing(BasicRequestValidation basicRequestValidation)
    {
      _basicRequestValidation = basicRequestValidation;
    }

    public void PerformFor(SubscriptionStartRequest subscriptionStartRequest)
    {
      _basicRequestValidation.PerformFor(subscriptionStartRequest);
    }
  }
}