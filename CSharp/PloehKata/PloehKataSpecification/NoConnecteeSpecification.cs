using NSubstitute;
using PloehKata;
using PloehKata.Logic;
using PloehKata.Ports;
using TddXt.AnyRoot.Exploding;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace PloehKataSpecification
{
    public class NoConnecteeSpecification
    {
        [Fact]
        public void ShouldReportOtherUserNotFoundWhenAttemptingConnectionAnyConnector()
        {
            //GIVEN
            var noConnectee = new NoConnectee();
            var connectionInProgress = Substitute.For<IConnectionInProgress>();

            //WHEN
            noConnectee.AttemptConnectionFrom(
                Any.Exploding<IExistingConnector>(), connectionInProgress);

            //THEN
            connectionInProgress.Received(1).OtherUserNotFound();
        }

    }
}