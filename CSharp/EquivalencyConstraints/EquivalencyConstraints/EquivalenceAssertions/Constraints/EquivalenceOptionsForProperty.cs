using System.Linq.Expressions;

namespace EquivalencyConstraints.EquivalenceAssertions.Constraints;

public class EquivalenceOptionsForProperty<T, TProperty>
{
    private readonly EquivalenceOptions<T> options;
    private readonly ExclusionRule rule;

    public EquivalenceOptionsForProperty(EquivalenceOptions<T> options, ExclusionRule rule)
    {
        this.options = options;
        this.rule = rule;
    }

    public EquivalenceOptions<T> Exclude(Expression<Func<TProperty, object>> excludeExpression)
    {
        var propertyName = GetPropertyName(excludeExpression);
        rule.ExcludedProperties.Add(propertyName);
        return options;
    }

    private string GetPropertyName(Expression<Func<TProperty, object>> expression)
    {
        if (expression.Body is MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }
        if (expression.Body is UnaryExpression { Operand: MemberExpression operand })
        {
            return operand.Member.Name;
        }
        throw new ArgumentException("Expression must be a simple member access, e.g., x => x.Property.");
    }
}