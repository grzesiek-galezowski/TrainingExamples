using AutofacContrib.NSubstitute;
using NSubstitute;
using NUnit.Framework;
using TddXt.AnyRoot.Numbers;
using static TddXt.AnyRoot.Root;

namespace AvoidAutoMockingContainers
{
    public class _02_WithSimpleAutoMock
    {
        [Test]
        public void ShouldSaveDataFromSourceInDestination__AutoMock()
        {
            //GIVEN
            using var autoSubstitute = new AutoSubstitute();
            var data = Any.Integer();
            autoSubstitute.Resolve<ISource>().Read().Returns(data);

            //WHEN
            autoSubstitute.Resolve<DataTransfer>().Commence();

            //THEN
            autoSubstitute.Resolve<IDestination>().Received(1).Save(data);
        }
    }
}