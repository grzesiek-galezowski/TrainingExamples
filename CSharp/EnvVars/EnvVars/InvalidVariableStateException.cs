using System;
using System.Collections.Immutable;
using System.Linq;

namespace EnvVars;

public class InvalidVariableStateException : Exception
{
  internal InvalidVariableStateException(
    OperationVerb operationVerb,
    VariableStates variableState, 
    VariableStates expectedState, 
    ImmutableList<VariableOperation> operationsLog)
  : base(
    $"{operationVerb} operation requires variable to be in state {expectedState}, " +
    $"but was in {variableState}." +
    Environment.NewLine +
    StackTraceHeader() +
    Environment.NewLine +
    Format(operationsLog))
  {
    
  }

  private static string StackTraceHeader()
  {
    return "Here are all the operations executed successfully " +
           "on the variable so far with their corresponding stack traces: ";
  }

  internal InvalidVariableStateException(
    OperationVerb operationVerb,
    VariableStates invalidState, 
    ImmutableList<VariableOperation> operationsLog)
  : base(
    $"You cannot {operationVerb} a variable in a {invalidState} state" +
    Environment.NewLine +
    StackTraceHeader() +
    Environment.NewLine +
    Format(operationsLog))
  {
    
  }

  private static string Format(ImmutableList<VariableOperation> operationsLog)
  {
    return string.Join(Environment.NewLine, operationsLog
      .Select(l =>
        $"+----{Environment.NewLine}" +
        "| " +
        l.OperationVerb +
        Environment.NewLine +
        $"+----{Environment.NewLine}" +
        FormatStackTrace(l.StackTrace)
        )) + $"{Environment.NewLine}+----"; //bug
  }

  private static string FormatStackTrace(string stackTrace)
  {
    return "|" + stackTrace.Replace(Environment.NewLine, Environment.NewLine + "|");
  }
}