using Validations.ThirdParty;

namespace Validations
{
  public class BasicRequestValidation : RequestValidation
  {
    private readonly Config _config;

    public BasicRequestValidation(Config config)
    {
      _config = config;
    }

    public void PerformFor(SubscriptionStartRequest request)
    {
      if (string.IsNullOrEmpty(request.Owner.Trim()))
      {
        throw new RequestValidationException();
      }

      if (string.IsNullOrEmpty(request.Target.Trim()))
      {
        throw new RequestValidationException();
      }

      if (request.Duration < _config.MaxDuration)
      {
        throw new RequestValidationException();
      }
    }
  }
}