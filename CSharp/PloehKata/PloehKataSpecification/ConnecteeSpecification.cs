using FluentAssertions;
using NSubstitute;
using PloehKata;
using TddXt.AnyRoot.Strings;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace PloehKataSpecification
{
    public class ConnecteeSpecification
    {
        [Fact]
        public void ShouldWHAT() //bug
        {
            //GIVEN
            var id = Any.String();
            var connectee = new Connectee(id);
            var existingConnector = Substitute.For<IExistingConnector>();
            var connectionInProgress = Any.Instance<IConnectionInProgress>();

            //WHEN
            connectee.AttemptConnectionFrom(existingConnector, connectionInProgress);

            //THEN
            existingConnector.Received(1).ConnectWith(id);
        }

    }
}