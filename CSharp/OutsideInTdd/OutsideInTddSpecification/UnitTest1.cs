using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using NUnit.Framework;
using OutsideInTdd.Adapters;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace OutsideInTddSpecification
{
    public class Tests
    {
        private static readonly Architecture Architecture =
            new ArchLoader().LoadAssemblies(
                    typeof(Startup).Assembly)
                .Build();

        [Test]
        public void Test1() //bug rename
        {
            Types().That().ResideInNamespace("**App**")
                .Should()
                .NotDependOnAnyTypesThat()
                .ResideInNamespace("**Adapters**")
                .Evaluate(Architecture);
        }
    }
}