using System.Collections;
using System.Reflection;
using NUnit.Framework.Constraints;

namespace EquivalencyConstraints.EquivalenceAssertions.Constraints;

public class EquivalentToConstraint<T>(T expected, EquivalenceOptions<T>? options = null) : Constraint
{
    private readonly EquivalenceOptions<T> options = options ?? new EquivalenceOptions<T>();

    public override ConstraintResult ApplyTo<TActual>(TActual actual)
    {
        var exclusionRules = options.GetExclusionRules();
        var matches = AreEquivalent(expected, actual, "", exclusionRules);
        return new ConstraintResult(this, actual, matches);
    }

    public override string Description => "equivalent to " + FormatForDescription(expected);

    private bool AreCollectionsEquivalent(IEnumerable expectedEnumerable, IEnumerable actualEnumerable,
        string currentPath, List<ExclusionRule> exclusionRules)
    {
        var rule = exclusionRules.FirstOrDefault(r => r.Path == currentPath);
        if (rule is { IgnoreOrder: true })
        {
            return AreCollectionsEquivalentIgnoringOrder(expectedEnumerable, actualEnumerable, currentPath,
                exclusionRules);
        }

        var expectedList = expectedEnumerable.Cast<object>().ToList();
        var actualList = actualEnumerable.Cast<object>().ToList();

        if (expectedList.Count != actualList.Count)
        {
            return false;
        }

        for (var i = 0; i < expectedList.Count; i++)
        {
            if (!AreEquivalent(expectedList[i], actualList[i], currentPath, exclusionRules))
                return false;
        }

        return true;
    }

    private bool AreCollectionsEquivalentIgnoringOrder(IEnumerable expected, IEnumerable actual, string currentPath, List<ExclusionRule> exclusionRules)
    {
        var expectedList = expected.Cast<object>().ToList();
        var actualList = actual.Cast<object>().ToList();

        if (expectedList.Count != actualList.Count)
            return false;

        var unmatchedActual = new List<object>(actualList);

        foreach (var exp in expectedList)
        {
            var found = false;
            for (var i = 0; i < unmatchedActual.Count; i++)
            {
                if (AreEquivalent(exp, unmatchedActual[i], currentPath, exclusionRules))
                {
                    unmatchedActual.RemoveAt(i);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                return false;
            }
        }
        return unmatchedActual.Count == 0;
    }

    private bool AreEquivalent(object? expectedObject, object? actualObject, string currentPath, List<ExclusionRule> exclusionRules)
    {
        // Handle null cases
        if (expectedObject == null && actualObject == null)
        {
            return true;
        }

        if (expectedObject == null || actualObject == null)
        {
            return false;
        }

        var expectedType = expectedObject.GetType();
        var actualType = actualObject.GetType();

        // Handle primitive types or types that override Equals
        if (TypeOverridesEquals(expectedType) && TypeOverridesEquals(actualType))
        {
            if (expectedType == actualType)
            {
                return Equals(expectedObject, actualObject);
            }
            return false;
        }

        // Handle collections
        if (expectedObject is IEnumerable expectedEnum and not string &&
            actualObject is IEnumerable actualEnum and not string)
        {
            return AreCollectionsEquivalent(expectedEnum, actualEnum, currentPath, exclusionRules);
        }

        // Compare properties
        var expectedProps = expectedType.GetProperties(BindingFlags.Public | BindingFlags.Instance) //bug only public and instance??
            .ToDictionary(p => p.Name);
        var actualProps = actualType.GetProperties(BindingFlags.Public | BindingFlags.Instance) //bug only public and instance??
            .ToDictionary(p => p.Name);

        var expectedPropNames = expectedProps.Keys.ToHashSet();
        var actualPropNames = actualProps.Keys.ToHashSet();

        if (!expectedPropNames.IsSubsetOf(actualPropNames))
        {
            return false;
        }

        // Apply exclusions for the current path
        var exclusionsForPath = exclusionRules.FirstOrDefault(r => r.Path == currentPath)?.ExcludedProperties ?? [];

        foreach (var propName in expectedPropNames)
        {
            if (!exclusionsForPath.Contains(propName))
            {
                // Skip excluded property
                var expectedProp = expectedProps[propName];
                var actualProp = actualProps[propName];

                if (expectedProp.PropertyType != actualProp.PropertyType)
                    return false;

                var expectedValue = expectedProp.GetValue(expectedObject);
                var actualValue = actualProp.GetValue(actualObject);

                var newPath = string.IsNullOrEmpty(currentPath) ? propName : $"{currentPath}.{propName}";

                if (!AreEquivalent(expectedValue, actualValue, newPath, exclusionRules))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool TypeOverridesEquals(Type type)
    {
        var equalsMethod = type.GetMethod("Equals", new[] { typeof(object) });
        return equalsMethod != null && equalsMethod.DeclaringType != typeof(object);
    }

    private string FormatForDescription(object obj)
    {
        if (obj == null)
            return "null";
        if (obj is string str)
            return $"\"{str}\"";
        if (obj is IEnumerable enumerable && obj is not string)
            return "< " + string.Join(", ", enumerable.Cast<object>().Select(FormatForDescription)) + " >";
        return obj.ToString();
    }
}