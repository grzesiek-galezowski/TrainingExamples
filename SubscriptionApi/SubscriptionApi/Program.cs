using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubscriptionApi.Authorization;
using SubscriptionApi.Commands;
using SubscriptionApi.ResponseBuilders;
using SubscriptionApi.Subscriptions;

namespace SubscriptionApi
{
  static class Program
  {
    static void Main(string[] args)
    {
      var structure = new AuthorizationStructure();

      var api = new Api(
        new CommandFactory(
          new SubscriptionResponseBuilder(), 
          new Subscriptions.Subscriptions(), 
          structure, 
          new SubscriptionFactory(), 
          new SubscriptionDataCorrectnessCriteria(), 
          new AssetQueriesFactory(
            structure
          )
        ), 
        new CommandFromApiProcessing(), 
        new DummyLog());
    }
  }
}
