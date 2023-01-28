using Extensions.Logging.NUnit;
using FluentAssertions;
using GreetingService;
using GreetingService.Services;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;

namespace AdapterTests;

public class Tests
{
  [Test]
  public async Task Test1()
  {
    var factory = new WebApplicationFactory<GreeterService>();
    var loggerFactory = new LoggerFactory();
    loggerFactory.AddProvider(new NUnitLoggerProvider());
    using var channel = GrpcChannel.ForAddress("https://localhost:7210", new GrpcChannelOptions
    {
      LoggerFactory = loggerFactory,
      HttpHandler = factory.Server.CreateHandler()
    });
    var client = new Greeter.GreeterClient(channel);
    var reply = await client.SayHelloAsync(
      new HelloRequest { Name = "GreeterClient" });
    reply.Message.Should().Be("Hello GreeterClient");
  }
}