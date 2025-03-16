using System.Linq.Expressions;

namespace EquivalencyConstraints.EquivalenceAssertions.Constraints;

public class EquivalenceOptionsForCollection<T, TU>
{
  private readonly EquivalenceOptions<T> _options;
  private readonly ExclusionRule _rule;

  public EquivalenceOptionsForCollection(EquivalenceOptions<T> options, ExclusionRule rule)
  {
    _options = options;
    _rule = rule;
  }

  public EquivalenceOptionsForCollection<T, TU> IgnoreOrder()
  {
    _rule.IgnoreOrder = true;
    return this;
  }

  public EquivalenceOptions<T> Exclude(Expression<Func<TU, object>> excludeExpression)
  {
    var propertyName = GetPropertyName(excludeExpression);
    _rule.ExcludedProperties.Add(propertyName);
    return _options; // Returns to root options for further configuration
  }

  private string GetPropertyName(Expression<Func<TU, object>> expression)
  {
    if (expression.Body is MemberExpression memberExpression)
    {
      return memberExpression.Member.Name;
    }
    if (expression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression operand)
    {
      return operand.Member.Name;
    }
    throw new ArgumentException("Expression must be a simple member access, e.g., x => x.Property.");
  }
}