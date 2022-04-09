using System;

namespace MockNoMockSpecification._03_MocksReturningMocks;

internal class MocksReturningMocksReturningMocks
{
  [Test]
  public void ShouldExtractExpiryDataFromConfig()
  {
    //GIVEN
    var config = Substitute.For<IAppConfiguration>();
    var builder = Substitute.For<IExpiryDataBuilder>();
    var cacheName = Any.String();
    var expiryPoint = Any.String();
    var index = Any.Integer();
    var extraction = new ConfigurationExtraction(config, cacheName, expiryPoint, index);
    var data = Any.Instance<ExpiryData>();
    var duration = Any.TimeSpan();
    var pollPeriod = Any.TimeSpan();

    config.GetCacheConfiguration(cacheName)
      .GetExpiryConfiguration(expiryPoint, index)
      .Duration.Returns(duration);
    config.GetCacheConfiguration(cacheName)
      .GetExpiryConfiguration(expiryPoint, index)
      .PollPeriod.Returns(pollPeriod);
    builder.WithDuration(duration)
      .WithPollPeriod(pollPeriod)
      .Build().Returns(data);

    //WHEN
    var result = extraction.ForExpiryUsing(builder);

    //THEN
    result.Should().Be(data);
  }
}

internal class ConfigurationExtraction
{
  private readonly IAppConfiguration _config;
  private readonly string _cacheName;
  private readonly string _expiryPoint;
  private readonly int _index;

  public ConfigurationExtraction(IAppConfiguration config, string cacheName, string expiryPoint, int index)
  {
    _config = config;
    _cacheName = cacheName;
    _expiryPoint = expiryPoint;
    _index = index;
  }

  public ExpiryData ForExpiryUsing(IExpiryDataBuilder builder)
  {
    var expiryConfiguration = _config
      .GetCacheConfiguration(_cacheName)
      .GetExpiryConfiguration(_expiryPoint, _index);
    return builder
      .WithDuration(expiryConfiguration.Duration)
      .WithPollPeriod(expiryConfiguration.PollPeriod)
      .Build();
  }
}

public interface IAppConfiguration
{
  ICacheConfiguration GetCacheConfiguration(string cacheName);
}

public interface ICacheConfiguration
{
  IExpiryConfiguration GetExpiryConfiguration(string expiryPoint, int index);
}

public interface IExpiryConfiguration
{
  TimeSpan PollPeriod { get; }
  TimeSpan Duration { get; }
}

public interface IExpiryDataBuilder
{
  IExpiryDataBuilder WithPollPeriod(TimeSpan period);
  IExpiryDataBuilder WithDuration(TimeSpan duration);
  ExpiryData Build();
}

public record ExpiryData(TimeSpan PollPeriod, TimeSpan Duration);