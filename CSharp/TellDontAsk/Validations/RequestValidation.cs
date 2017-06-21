using Validations.ThirdParty;

namespace Validations
{
  interface RequestValidation
  {
    void PerformFor(SubscriptionStartRequest request);
  }
}