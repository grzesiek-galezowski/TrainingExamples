using Extensions.Logging.NUnit;
using FluentAssertions;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using GreetingService.Services;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using NVidiaAsr;
using TddXt.AnyExtensibility;
using TddXt.AnyRoot;
using TddXt.TypeResolution;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic.ImplementationDetails;
using static TddXt.AnyRoot.Root;

namespace AdapterTests;

public class Tests
{
  [Test]
  public async Task Test1()
  {
    var withDefaultLimits = DefaultGenerationRequest.WithDefaultLimits();
    var x = ((AllGenerator)Root.Any).Instance<SpeechRecognitionResult>(withDefaultLimits);

    var y = new SpeechRecognitionResult()
    {
      Alternatives = { new SpeechRecognitionAlternative() }
    }

    x.Alternatives.Count.Should().Be(3);
    x.AudioProcessed.Should().NotBe(0);
    await using var factory = new WebApplicationFactory<AsrService>();
    var loggerFactory = new LoggerFactory();
    loggerFactory.AddProvider(new NUnitLoggerProvider());
    using var channel = GrpcChannel.ForAddress("https://localhost:7210", new GrpcChannelOptions
    {
      LoggerFactory = loggerFactory,
      HttpHandler = factory.Server.CreateHandler()
    });
    var client = new RivaSpeechRecognition.RivaSpeechRecognitionClient(channel);
    var reply = await client.GetRivaSpeechRecognitionConfigAsync(new RivaSpeechRecognitionConfigRequest
    {
      ModelName = "lol"
    });
    reply.ModelConfig.Should().BeEquivalentTo(new RepeatedField<RivaSpeechRecognitionConfigResponse.Types.Config>
    {
      new RivaSpeechRecognitionConfigResponse.Types.Config()
      {
        ModelName = "lol"
      }
    });
  }
}