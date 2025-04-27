using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.NUnit;
using NUnit.Framework;
using OutsideInTdd.Adapters;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace OutsideInTddSpecification
{
    public class ArchitectureSpecification
    {
        private static readonly Architecture Architecture =
            new ArchLoader().LoadAssemblies(
                    typeof(Startup).Assembly)
                .Build();

        [Test]
        public void ShouldNotContainDependenciesFromAppToAdapters()
        {
            Types().That().ResideInNamespace("App")
                .Should()
                .NotDependOnAnyTypesThat()
                .ResideInNamespace("Adapters")
                .Check(Architecture);
        }
    }
}