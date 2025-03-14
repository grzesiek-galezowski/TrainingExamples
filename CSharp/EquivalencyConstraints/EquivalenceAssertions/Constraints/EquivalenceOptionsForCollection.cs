using System.Linq.Expressions;

namespace DependencyUpdatePriorityScanner.EquivalenceAssertions.Constraints;

public class EquivalenceOptionsForCollection<T, U>
{
    private readonly EquivalenceOptions<T> options;
    private readonly ExclusionRule rule;

    public EquivalenceOptionsForCollection(EquivalenceOptions<T> options, ExclusionRule rule)
    {
        this.options = options;
        this.rule = rule;
    }

    public EquivalenceOptionsForCollection<T, U> IgnoreOrder()
    {
        rule.IgnoreOrder = true;
        return this;
    }

    public EquivalenceOptions<T> Exclude(Expression<Func<U, object>> excludeExpression)
    {
        var propertyName = GetPropertyName(excludeExpression);
        rule.ExcludedProperties.Add(propertyName);
        return options; // Returns to root options for further configuration
    }

    private string GetPropertyName(Expression<Func<U, object>> expression)
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