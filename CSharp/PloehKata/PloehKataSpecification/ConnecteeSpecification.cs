using NSubstitute;
using PloehKata;
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
            var userDto = Any.Instance<UserDto>();
            var connectee = new Connectee(userDto);
            var existingConnector = Substitute.For<IExistingConnector>();
            var connectionInProgress = Any.Instance<IConnectionInProgress>();

            //WHEN
            connectee.AttemptConnectionFrom(existingConnector, connectionInProgress);

            //THEN
            existingConnector.Received(1).AddConnectionId(userDto.Id);
        }

    }
}