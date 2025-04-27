using FluentAssertions;
using PloehKata;
using PloehKata.Adapters;
using Xunit;

namespace PloehKataSpecification
{
  public class ConnectionInProgressFactorySpecification
  {
    [Fact]
    public void ShouldCreateEmptyConnectionInProgress()
    {
      //GIVEN
      var factory = new ConnectionInProgressFactory();

      //WHEN
      var connectionInProgress = factory.CreateConnectionInProgress();

      //THEN
      connectionInProgress.Should().BeOfType<JsonBasedConnectionInProgress>();
    }
  }
}