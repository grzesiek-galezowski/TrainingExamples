using System.Linq.Expressions;

namespace EquivalencyConstraints.EquivalenceAssertions.Constraints;

public static class PropertyPathConversions
{
  public static string GetPropertyPathFrom<TProperty, T>(Expression<Func<T, TProperty>> expression)
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
          var indexArgument = methodCallExpression.Arguments.FirstOrDefault();
          if (indexArgument is ConstantExpression constantExpression)
          {
            return $"{objectMemberExpression.Member.Name}[{constantExpression.Value}]";
          }
          return objectMemberExpression.Member.Name;
        }
        else if (methodCallExpression.Object is ParameterExpression parameterExpression)
        {
          // Handle the case where the indexer is used directly on the parameter
          var indexArgument = methodCallExpression.Arguments.FirstOrDefault();
          if (indexArgument is ConstantExpression constantExpression)
          {
            return $"{parameterExpression.Name}[{constantExpression.Value}]";
          }
          return parameterExpression.Name ?? throw new InvalidOperationException("indexer parameter expression name cannot be null");
        }
      }
      // Handle method call expressions like o.First()
      return methodCallExpression.Method.Name;
    }
    throw new ArgumentException("Expression must be a simple member access, e.g., x => x.Property.");
  }
}