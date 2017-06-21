using System.Collections.Generic;

namespace Command.Results
{
  public class ConcreteAggregateResult<T> : AggregateResult<T>
  {
    public IList<T> Value { get; } = new List<T>();
  }
}