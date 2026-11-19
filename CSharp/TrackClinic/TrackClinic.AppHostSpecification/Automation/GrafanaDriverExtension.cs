using System.Collections.Immutable;
using System.Text.Json;
using AwesomeAssertions;
using Flurl.Http;

namespace TrackClinic.AppHostSpecification.Automation;

public class GrafanaDriverExtension(FlurlClient grafanaClient, CancellationToken cancellationToken)
{
  public async Task ShouldHaveProvisionedDatasources(ImmutableList<string> expectedDatasourceNames)
  {
    var response = await grafanaClient
      .Request("api", "datasources")
      .WithBasicAuth("admin", "admin")
      .GetJsonAsync<JsonElement>(cancellationToken: cancellationToken);

    var datasourceNames = response.EnumerateArray()
      .Select(ds => ds.GetProperty("name").GetString())
      .Where(name => name is not null)
      .ToImmutableList();

    datasourceNames.Should().Contain(expectedDatasourceNames);
  }

  public async Task ShouldHaveProvisionedDashboards(ImmutableList<string> expectedDashboardTitles)
  {
    var response = await grafanaClient
      .Request("api", "search")
      .SetQueryParams(new { type = "dash-db" })
      .WithBasicAuth("admin", "admin")
      .GetJsonAsync<JsonElement>(cancellationToken: cancellationToken);

    var dashboardTitles = response.EnumerateArray()
      .Select(d => d.GetProperty("title").GetString())
      .Where(title => title is not null)
      .ToImmutableList();

    dashboardTitles.Should().Contain(expectedDashboardTitles);
  }
}
