using NSubstitute;
using PloehKata;
using PloehKata.Logic;
using PloehKata.Ports;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace PloehKataSpecification
{
    public class ConnecteeSpecification
    {
        [Fact]
        public void ShouldAddConnectionToConnectorAndReportSuccessWhenConnectionAttemptIsMade()
        {
            //GIVEN
            var userDto = Any.Instance<UserDto>();
            var connectee = new Connectee(userDto);
            var existingConnector = Substitute.For<IExistingConnector>();
            var connectionInProgress = Substitute.For<IConnectionInProgress>();

            //WHEN
            connectee.AttemptConnectionFrom(existingConnector, connectionInProgress);

            //THEN
            Received.InOrder(() =>
            {
                existingConnector.AddConnection(userDto);
                connectionInProgress.Success(userDto);
            });
        }
    }
}