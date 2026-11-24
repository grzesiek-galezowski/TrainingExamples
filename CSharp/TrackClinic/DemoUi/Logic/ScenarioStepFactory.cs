using System.Collections.Immutable;
using DemoUi.Dto;
using Flurl.Http;

namespace DemoUi.Logic;

public static class ScenarioStepFactory
{
  public static ImmutableList<ScenarioStep> CreateScenarioSteps(
    string artist,
    string song1,
    string song2,
    IFlurlClient flurlClient,
    TimeSpan timeout)
  {
    return
    [
      new ScenarioStep(
        flurlClient,
        timeout,
        stepNumber: 1,
        actionName: $"Create Song 1 ({song1})",
        method: HttpMethod.Post,
        relativeUrl: $"/api/v1/artists/{Uri.EscapeDataString(artist)}/songs",
        content: new CreateSongRequestDto(song1)),
      new ScenarioStep(
        flurlClient,
        timeout,
        stepNumber: 2,
        actionName: $"Create Song 2 ({song2})",
        method: HttpMethod.Post,
        relativeUrl: $"/api/v1/artists/{Uri.EscapeDataString(artist)}/songs",
        content: new CreateSongRequestDto(song2)),
      new ScenarioStep(
        flurlClient,
        timeout,
        stepNumber: 3,
        actionName: $"Get Song 1 ({song1})",
        method: HttpMethod.Get,
        relativeUrl: $"/api/v1/artists/{Uri.EscapeDataString(artist)}/songs/{Uri.EscapeDataString(song1)}",
        content: null),
      new ScenarioStep(
        flurlClient,
        timeout,
        stepNumber: 4,
        actionName: $"Get Song 2 ({song2})",
        method: HttpMethod.Get,
        relativeUrl: $"/api/v1/artists/{Uri.EscapeDataString(artist)}/songs/{Uri.EscapeDataString(song2)}",
        content: null)
    ];
  }
}