using System.Linq.Expressions;

namespace EquivalencyConstraints.EquivalenceAssertions.Constraints;

public class EquivalenceOptions<T>
{
  private readonly List<ExclusionRule> _exclusionRules = new();

  // For non-collection properties
  public EquivalenceOptionsForProperty<T, TProperty> For<TProperty>(Expression<Func<T, TProperty>> pathExpression)
  {
    var path = PropertyPathConversions.GetPropertyPathFrom(pathExpression);
    var rule = new ExclusionRule(path);
    _exclusionRules.Add(rule);
    return new EquivalenceOptionsForProperty<T, TProperty>(this, rule);
  }

  // For collection properties
  public EquivalenceOptionsForCollection<T, TU> ForCollection<TU>(Expression<Func<T, IEnumerable<TU>>> pathExpression)
  {
    var path = PropertyPathConversions.GetPropertyPathFrom(pathExpression);
    var rule = new ExclusionRule(path);
    _exclusionRules.Add(rule);
    return new EquivalenceOptionsForCollection<T, TU>(this, rule);
  }

  internal List<ExclusionRule> GetExclusionRules() => _exclusionRules;
}
