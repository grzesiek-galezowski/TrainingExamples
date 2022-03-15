using FluentAssertions;
using NUnit.Framework;

namespace AntiAntiMockSpecification;

public class StructuralTests
{

    [Test]
    public void ShouldBeDecoratedWithTests()
    {
        this.GetType().GetMethod(nameof(ShouldBeDecoratedWithTests)).Should().BeDecoratedWith<TestAttribute>();
        this.GetType().GetMethod(nameof(ShouldBeDecoratedWithTests)).Should().NotBeAsync();
        this.GetType().GetMethod(nameof(ShouldBeDecoratedWithTests)).Should().NotBeVirtual();
    }

}