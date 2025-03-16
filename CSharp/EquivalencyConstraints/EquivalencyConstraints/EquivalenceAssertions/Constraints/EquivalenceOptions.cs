using System.Linq.Expressions;

namespace EquivalencyConstraints.EquivalenceAssertions.Constraints;

public class EquivalenceOptions<T>
{
  private readonly List<ExclusionRule> _exclusionRules = new();

  // For non-collection properties
  public EquivalenceOptionsForProperty<T, TProperty> For<TProperty>(Expression<Func<T, TProperty>> pathExpression)
  {
    var path = GetPropertyPath(pathExpression);
    var rule = new ExclusionRule(path);
    _exclusionRules.Add(rule);
    return new EquivalenceOptionsForProperty<T, TProperty>(this, rule);
  }

  // For collection properties
  public EquivalenceOptionsForCollection<T, TU> ForCollection<TU>(Expression<Func<T, IEnumerable<TU>>> pathExpression)
  {
    var path = GetPropertyPath(pathExpression);
    var rule = new ExclusionRule(path);
    _exclusionRules.Add(rule);
    return new EquivalenceOptionsForCollection<T, TU>(this, rule);
  }

  internal List<ExclusionRule> GetExclusionRules() => _exclusionRules;

  private string GetPropertyPath<TProperty>(Expression<Func<T, TProperty>> expression)
  {
    if (expression.Body is MemberExpression memberExpression)
    {
      return memberExpression.Member.Name;
    }
    else if (expression.Body is MethodCallExpression methodCallExpression)
    {
      // Handle indexer expressions
      if (methodCallExpression.Method.Name == "get_Item")
      {
        // Assuming the indexer is used on a property, get the property name
        if (methodCallExpression.Object is MemberExpression objectMemberExpression)
        {
          return objectMemberExpression.Member.Name;
        }
        else if (methodCallExpression.Object is ParameterExpression parameterExpression)
        {
          // Handle the case where the indexer is used directly on the parameter
          return parameterExpression.Name ??
                 throw new InvalidOperationException("indexer parameter expression name cannot be null");
        }
      }
      // Handle method call expressions like o.First()
      return methodCallExpression.Method.Name;
    }
    throw new ArgumentException("Expression must be a simple member access, e.g., x => x.Property.");
  }
}
