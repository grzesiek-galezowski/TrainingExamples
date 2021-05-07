using NSubstitute;
using NUnit.Framework;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Numbers;

namespace AvoidAutoMockingContainers
{
    public class _01_WithoutAutoMock
    {
        [Test]
        public void ShouldSaveDataFromSourceInDestination()
        {
            //GIVEN
            var data = Root.Any.Integer();
            var source = Substitute.For<ISource>();
            var destination = Substitute.For<IDestination>();
            var manager = new TransferUtilHelperManager(destination, source);

            source.Read().Returns(data);

            //WHEN
            manager.TransferData();

            //THEN
            destination.Received(1).Save(data);
        }
    }
}