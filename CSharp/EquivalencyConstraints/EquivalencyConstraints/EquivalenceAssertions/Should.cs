using EquivalencyConstraints.EquivalenceAssertions.Constraints;

namespace EquivalencyConstraints.EquivalenceAssertions;

public static class Should
{
    public static EquivalentToConstraint<T> BeEquivalentTo<T>(T expected, Action<EquivalenceOptions<T>>? configureOptions)
    {
        configureOptions ??= _ => { };
        var options = new EquivalenceOptions<T>();
        configureOptions.Invoke(options);
        return new EquivalentToConstraint<T>(expected, options);
    }

    public static EquivalentToConstraint<T> BeEquivalentTo<T>(T expected)
    {
        return new EquivalentToConstraint<T>(expected);
    }
}