using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validations.ThirdParty;

namespace Validations
{
    public class Main
    {
      public void Program()
      {
        var requestProcessing = new RequestProcessing(
          new BasicRequestValidation(
            new InMemoryConfig()
            {
              MaxDuration = 4000
            }));



        var subscriptionStartRequest = new SubscriptionStartRequest()
        {
          Owner = "Zenek",
          Target = "dev01",
          Duration = 200
        };

        requestProcessing.PerformFor(subscriptionStartRequest);

      }
    }

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
