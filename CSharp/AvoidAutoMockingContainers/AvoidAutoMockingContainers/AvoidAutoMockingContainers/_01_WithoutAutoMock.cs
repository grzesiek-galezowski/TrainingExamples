using NSubstitute;
using NUnit.Framework;
using TddXt.AnyRoot.Numbers;
using static TddXt.AnyRoot.Root;

namespace AvoidAutoMockingContainers
{
    public class _01_WithoutAutoMock
    {
        [Test]
        public void ShouldSaveDataFromSourceInDestination()
        {
            //GIVEN
            var data = Any.Integer();
            var source = Substitute.For<ISource>();
            var destination = Substitute.For<IDestination>();
            var transfer = new DataTransfer(destination, source);

            source.Read().Returns(data);

            //WHEN
            transfer.Commence();

            //THEN
            destination.Received(1).Save(data);
        }
    }
}