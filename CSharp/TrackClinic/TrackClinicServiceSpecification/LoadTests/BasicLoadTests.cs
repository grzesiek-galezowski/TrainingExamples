using LoadSurge.Models;
using LoadSurge.Runner;
using TrackClinic;
using TrackClinicServiceSpecification.Automation;

namespace TrackClinicServiceSpecification.LoadTests;

[NonParallelizable]
public class BasicLoadTests
{
  [Test]
  public async Task ShouldServeHealthEndpointsUnderLightConcurrentLoad()
  {
    await using var driver = await TrackClinicDriver.CreateInstance();
    await driver.Start();

    var executionPlan = new LoadExecutionPlan
    {
      Name = "Health_Endpoints_Under_Load",
      Settings = new LoadSettings
      {
        Duration = TimeSpan.FromSeconds(2),
        Interval = TimeSpan.FromMilliseconds(200),
        Concurrency = 5,
        TerminationMode = TerminationMode.CompleteCurrentInterval,
        GracefulStopTimeout = TimeSpan.FromSeconds(2)
      },
      Action = async () =>
      {
        var live = await driver.RequestHealthCheck("/health/live");
        await live.ShouldBeHealthy();

        var ready = await driver.RequestHealthCheck("/health/ready");
        await ready.ShouldBeHealthy();

        return true;
      }
    };

    var result = await LoadRunner.Run(executionPlan);

    Assert.That(result.Total, Is.GreaterThanOrEqualTo(20));
    Assert.That(result.Success, Is.EqualTo(result.Total));
    Assert.That(result.Failure, Is.EqualTo(0));

    Print(result);
  }

  [Test]
  public async Task ShouldCreateAndRetrieveSongsUnderModerateConcurrentLoad()
  {
    await using var driver = await TrackClinicDriver.CreateInstance();
    await driver.Start();

    var requestCounter = 0;
    var executionPlan = new LoadExecutionPlan
    {
      Name = "Create_And_Retrieve_Songs_Under_Load",
      Settings = new LoadSettings
      {
        Duration = TimeSpan.FromSeconds(2),
        Interval = TimeSpan.FromMilliseconds(250),
        Concurrency = 4,
        TerminationMode = TerminationMode.CompleteCurrentInterval,
        GracefulStopTimeout = TimeSpan.FromSeconds(2)
      },
      Action = async () =>
      {
        var artistName = $"artist-{Interlocked.Increment(ref requestCounter)}-{Guid.NewGuid():N}";
        var songName = $"song-{Guid.NewGuid():N}";

        await driver.CreateSongProject(artistName, new CreateSongRequestDto(songName));

        var response = await driver.RetrieveProjectDetails(artistName, songName);
        await response.ShouldHaveContentEquivalentTo(SongDetailsDto.Initial(artistName, songName));

        return true;
      }
    };

    var result = await LoadRunner.Run(executionPlan);

    Assert.That(result.Total, Is.GreaterThanOrEqualTo(10));
    Assert.That(result.Success, Is.EqualTo(result.Total));
    Assert.That(result.Failure, Is.EqualTo(0));

    Print(result);
  }

  private static void Print(LoadResult result)
  {
    Console.WriteLine($"Total: {result.Total}, Success: {result.Success}, Failed: {result.Failure}");
    Console.WriteLine($"RPS: {result.RequestsPerSecond:F1}, Avg: {result.AverageLatency:F1}ms, P95: {result.Percentile95Latency:F1}ms");
  }
}
