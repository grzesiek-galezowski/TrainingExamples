using AtmaFileSystem;
using AtmaFileSystem.IO;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AntiAntiMockSpecification;

internal class WorkProcessingSpecification
{
    [Test]
    public void ShouldContainItsOwnText()
    {
        //WHEN
        var fileText = AbsoluteFilePath.OfThisFile()
            .ParentDirectory()
            .AddFileName("WorkProcessing.cs")
            .ReadAllText();

        //THEN
        fileText.Should().Be(
            @"namespace AntiAntiMockSpecification;

public class WorkProcessing
{
    private readonly IFirstPart _firstPart;
    private readonly ISecondPart _secondPart;

    public WorkProcessing(IFirstPart firstPart, ISecondPart secondPart)
    {
        _firstPart = firstPart;
        _secondPart = secondPart;
    }

    public void Process(Work work)
    {
        _firstPart.Process(work);
        _secondPart.Process(work);
    }
}");
    }

    [Test]
    public void ShouldBrodcastWorkToItsPartsInOrder()
    {
        //GIVEN
        var firstPart = Substitute.For<IFirstPart>();
        var secondPart = Substitute.For<ISecondPart>();
        var workProcessing = new WorkProcessing(firstPart, secondPart);
        var work = new Work();

        //WHEN
        workProcessing.Process(work);

        //THEN
        Received.InOrder(() =>
        {
            firstPart.Process(work);
            secondPart.Process(work);
        });
    }
}