using System.Threading.Tasks;
using Flurl.Http;
using PactNet;

namespace ConsumerDrivenContractTests;

static internal class ApkaKonsument
{
    public static async Task<ConsumerTests.ResponseDto> WykonajCoś(IConsumerContext ctx)
    {
        var client = new FlurlClient(ctx.MockServerUri.AbsoluteUri);
        var response = await client.Request("test")
            .WithHeader("Accept", "application/json")
            .GetJsonAsync<ConsumerTests.ResponseDto>();
        return response;
    }
}