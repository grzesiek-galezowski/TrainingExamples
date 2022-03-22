using System.Reflection;
using FluentAssertions;
using NUnit.Framework;

namespace AntiAntiMockSpecification;

public class StructuralTests
{

    [Test]
    public void ShouldBeDecoratedWithTests()
    {
        GetType().GetMethod(nameof(ShouldBeDecoratedWithTests))
            .Should().BeDecoratedWith<TestAttribute>();
        GetType().GetMethod(nameof(ShouldBeDecoratedWithTests))
            .Should().NotBeAsync();
        GetType().GetMethod(nameof(ShouldBeDecoratedWithTests))
            .Should().NotBeVirtual();
    }

    [Test]
    public void ShouldReferenceProductionCode()
    {
        Assembly.GetExecutingAssembly().Should().Reference(Assembly.GetAssembly(typeof(Program)));
    }

}