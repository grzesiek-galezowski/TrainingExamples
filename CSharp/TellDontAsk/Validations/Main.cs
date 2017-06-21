using Validations.ThirdParty;

namespace Validations
{
    public class Main
    {
      public void Program()
      {
        var requestProcessing = Resolve();
        ActOn(requestProcessing);
      }

      private static void ActOn(RequestProcessing requestProcessing)
      {
        var subscriptionStartRequest = new SubscriptionStartRequest()
        {
          Owner = "Zenek",
          Target = "dev01",
          Duration = 200
        };

        requestProcessing.PerformFor(subscriptionStartRequest);
      }

      private static RequestProcessing Resolve()
      {
        return new RequestProcessing(
          new BasicRequestValidation(
            new InMemoryConfig()
            {
              MaxDuration = 4000
            }));
      }
    }
}
