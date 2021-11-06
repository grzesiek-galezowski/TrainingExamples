using System;

namespace EnvVars;

internal record VariableOperation
{
  public VariableOperation(OperationVerb operationVerb, string stackTrace)
  {
    OperationVerb = operationVerb;
    var indexOfFirstLineEnd = stackTrace.IndexOf(Environment.NewLine, StringComparison.Ordinal);
    StackTrace = stackTrace.Remove(0, indexOfFirstLineEnd + Environment.NewLine.Length);
  }

  public OperationVerb OperationVerb { get; init; }
  public string StackTrace { get; init; }
}