using NSubstitute;
using NSubstitute.ExceptionExtensions;
using PloehKata;
using PloehKata.Logic;
using PloehKata.Ports;
using TddXt.AnyRoot.Strings;
using TddXt.XNSubstitute.Root;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace PloehKataSpecification
{
    public class ConnectionUseCaseSpecification
    {
        [Fact]
        public void ShouldReadBothUsersAndAttemptConnectionBetweenThemThenUpdateConector()
        {
            //GIVEN
            var connectionInProgress = Any.Instance<IConnectionInProgress>();
            var user1Id = Any.String();
            var user2Id = Any.String();
            var lookup = Substitute.For<IUserLookup>();
            var destination = Substitute.For<IConnectorDestination>();
            var connector = Substitute.For<IConnector>();
            var connectee = Any.Instance<IConnectee>();
            var command = new ConnectionUseCase(connectionInProgress, user1Id, user2Id, lookup, destination);

            lookup.LookupConnector(user1Id).Returns(connector);
            lookup.LookupConnectee(user2Id).Returns(connectee);

            //WHEN
            command.Execute();

            //THEN
            Received.InOrder(() =>
            {
                connector.AttemptConnectionWith(connectee, connectionInProgress);
                connector.WriteTo(destination);
            });
        }

        [Fact]
        public void ShouldReportInvalidUserIdWhenInvalidConnectorIdExceptionIsThrownFromLookup()
        {
            //GIVEN
            var connectionInProgress = Substitute.For<IConnectionInProgress>();
            var user1Id = Any.String();
            var user2Id = Any.String();
            var lookup = Substitute.For<IUserLookup>();
            var destination = Substitute.For<IConnectorDestination>();
            var command = new ConnectionUseCase(connectionInProgress, user1Id, user2Id, lookup, destination);

            lookup.LookupConnector(user1Id).Throws(Any.Instance<InvalidConnectorIdException>());

            //WHEN
            command.Execute();

            //THEN
            XReceived.Only(() => connectionInProgress.Received(1).InvalidUserId());
        }

        [Fact]
        public void ShouldReportInvalidOtherUserIdWhenInvalidConnecteeIdExceptionIsThrownFromLookup()
        {
            //GIVEN
            var connectionInProgress = Substitute.For<IConnectionInProgress>();
            var user1Id = Any.String();
            var user2Id = Any.String();
            var lookup = Substitute.For<IUserLookup>();
            var destination = Substitute.For<IConnectorDestination>();
            var command = new ConnectionUseCase(connectionInProgress, user1Id, user2Id, lookup, destination);

            lookup.LookupConnector(user1Id).Throws(Any.Instance<InvalidConnecteeIdException>());

            //WHEN
            command.Execute();

            //THEN
            XReceived.Only(() => connectionInProgress.Received(1).InvalidOtherUserId());
        }

        //bug invalid ID -> exception?
    }
}