package other;

import Dto.NewSubscriptionParametersDto;
import Dto.StoppedSubscriptionParametersDto;using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubscriptionApi.Authorization;
using SubscriptionApi.Commands;
using SubscriptionApi.Dto;
using SubscriptionApi.ResponseBuilders;
using SubscriptionApi.Subscriptions;

namespace SubscriptionApi
{
  static class Program
  {
    static void Main(string[] args)
    {
      var structure = new AuthorizationStructure();

      var dummyLog = new DummyLog();
      var api = new Api(
        new CommandFactory(
          new Subscriptions.Subscriptions(),
          structure, 
          new SubscriptionFactory(), 
          new SubscriptionDataCorrectnessCriteria(), 
          new AssetQueriesFactory(
            structure
          ),
          dummyLog
        ), 
        new DefaultResponseBuilderFactory(), 
        dummyLog
      );

      var response1 = api.StartSubscription(new NewSubscriptionParametersDto());
      var response2 = api.StopSubscription(new StoppedSubscriptionParametersDto());
    }
  }
}
