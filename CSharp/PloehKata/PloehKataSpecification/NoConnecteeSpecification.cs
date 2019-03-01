using FluentAssertions;
using NSubstitute;
using NSubstitute.Core.Arguments;
using PloehKata;
using TddXt.AnyRoot.Exploding;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace PloehKataSpecification
{
    public class NoConnecteeSpecification
    {
        [Fact]
        public void ShouldWHAT()
        {
            //GIVEN
            var noConnectee = new NoConnectee();
            var connectionInProgress = Substitute.For<IConnectionInProgress>();

            //WHEN
            noConnectee.AttemptConnectionFrom(
                Any.Exploding<IExistingConnector>(), connectionInProgress);

            //THEN
            true.Should().BeFalse("not implemented");
        }

    }
}