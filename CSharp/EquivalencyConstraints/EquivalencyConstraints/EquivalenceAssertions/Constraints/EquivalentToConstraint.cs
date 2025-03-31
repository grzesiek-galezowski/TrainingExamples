using System.Collections;
using System.Reflection;
using NUnit.Framework.Constraints;
using ServiceStack.Text;

namespace EquivalencyConstraints.EquivalenceAssertions.Constraints;

public record TraversePath(string CurrentPath)
{
  public TraversePath Append(string propName)
  {
    return new TraversePath(CurrentPath: string.IsNullOrEmpty(CurrentPath) ? propName : $"{CurrentPath}.{propName}");
  }

  public bool Is(string testedPath)
  {
    return testedPath == CurrentPath;
  }
}

public class EquivalentToConstraint<T>(T expected, EquivalenceOptions<T>? options = null) : Constraint
{
  private readonly EquivalenceOptions<T> _options = options ?? new EquivalenceOptions<T>();

  public override ConstraintResult ApplyTo<TActual>(TActual actual)
  {
    var exclusionRules = _options.GetExclusionRules();
    var matches = AreEquivalent(expected, actual, new TraversePath(""), exclusionRules);
    return new ConstraintResult(this, ObjectWithToString.ObjectForOutput(actual), matches);
  }

  public override string Description => "equivalent to " + ObjectWithToString.ObjectForOutput(expected);

  private bool AreCollectionsEquivalent(IEnumerable expectedEnumerable, IEnumerable actualEnumerable,
      TraversePath currentPath, List<ExclusionRule> exclusionRules)
  {
    var rule = exclusionRules.FirstOrDefault(r => currentPath.Is(r.Path));
    if (rule is { IgnoreOrder: true })
    {
      return AreCollectionsEquivalentIgnoringOrder(expectedEnumerable, actualEnumerable, currentPath,
          exclusionRules);
    }
    else
    {
      return AreCollectionsEquivalentInOrder(expectedEnumerable, actualEnumerable, currentPath, exclusionRules);
    }
  }

  private bool AreCollectionsEquivalentInOrder(
    IEnumerable expectedEnumerable, IEnumerable actualEnumerable, TraversePath currentPath, List<ExclusionRule> exclusionRules)
  {
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

  private bool AreCollectionsEquivalentIgnoringOrder(IEnumerable expected, IEnumerable actual, TraversePath currentPath, List<ExclusionRule> exclusionRules)
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

  private bool AreEquivalent(object? expectedObject, object? actualObject, TraversePath currentPath, List<ExclusionRule> exclusionRules)
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
    var expectedProps = GetPropertiesByName(expectedType);
    var actualProps = GetPropertiesByName(actualType);

    var expectedPropNames = expectedProps.Keys.ToHashSet();
    var actualPropNames = actualProps.Keys.ToHashSet();

    if (!expectedPropNames.IsSubsetOf(actualPropNames))
    {
      return false;
    }

    // Apply exclusions for the current path
    var exclusionsForPath = exclusionRules.FirstOrDefault(r => currentPath.Is(r.Path))?.ExcludedProperties ?? [];

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

        var newPath = currentPath.Append(propName);

        if (!AreEquivalent(expectedValue, actualValue, newPath, exclusionRules))
        {
          return false;
        }
      }
    }

    return true;
  }

  private static Dictionary<string, PropertyInfo> GetPropertiesByName(Type expectedType)
  {
    return expectedType.GetProperties(BindingFlags.Public | BindingFlags.Instance) //bug only public and instance??
      .ToDictionary(p => p.Name);
  }


  private bool TypeOverridesEquals(Type type)
  {
    var equalsMethod = type.GetMethod("Equals", [typeof(object)]);
    return equalsMethod != null && equalsMethod.DeclaringType != typeof(object);
  }
}

public class ObjectWithToString
{
  private readonly object _actual;

  public ObjectWithToString(object actual)
  {
    _actual = actual;
  }

  public override string ToString()
  {
    return FormatForDescription(_actual);
  }

  private string FormatForDescription(object? obj)
  {
    if (obj == null)
      return "null";
    if (obj is string str)
      return $"\"{str}\"";
    if (obj is IEnumerable enumerable and not string)
      return "< " + string.Join(", ", enumerable.Cast<object>().Select(FormatForDescription)) + " >";
    return obj.Dump() ?? obj.GetType().ToString();
  }

  public static object ObjectForOutput<TActual>(TActual actual)
  {
    return HasOverriddenToString(typeof(TActual)) ? actual : new ObjectWithToString(actual);
  }

  private static bool HasOverriddenToString(Type type)
  {
    return type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
      .Any(m => m.Name == "ToString" && m.GetParameters().Length == 0 && m.DeclaringType != typeof(object));
  }
}