using System;
using System.Collections.Immutable;
using Functional.Maybe;
using Functional.Maybe.Just;

namespace EnvVars;

internal class VariableContainer<T>
{
  public ImmutableList<VariableOperation> OperationsLog { get; private set; } 
    = ImmutableList<VariableOperation>.Empty;
  private readonly Func<T, string> _deparser;
  private readonly Func<string, T> _parser;
  private readonly EnvironmentVariableTarget _target;
  public Maybe<T> Value { get; private set; } = Maybe<T>.Nothing;
  public string Name { get; }
  public VariableStates _state = VariableStates.Unknown;

  public static VariableContainer<T> ProcessWide(
    string name, 
    Func<string, T> parser, 
    Func<T, string> deparser)
  {
    return new VariableContainer<T>(name, parser, deparser, EnvironmentVariableTarget.Process);
  }

  public static VariableContainer<T> MachineWide(
    string name, 
    Func<string, T> parser, 
    Func<T, string> deparser)
  {
    return new VariableContainer<T>(name, parser, deparser, EnvironmentVariableTarget.Machine);
  }

  private VariableContainer(
    string name,
    Func<string, T> parser,
    Func<T, string> deparser,
    EnvironmentVariableTarget target)
  {
    Name = name;
    _parser = parser;
    _deparser = deparser;
    _target = target;
    OperationsLog = OperationsLog.Add(
      new VariableOperation(OperationVerb.CreateContainer, Environment.StackTrace));
  }

  public void Delete()
  {
    AssertStateIs(VariableStates.Defined, OperationVerb.Delete);
    Environment.SetEnvironmentVariable(Name, null, _target);
    Value = Maybe<T>.Nothing;
    _state = VariableStates.Deleted;
    OperationsLog = OperationsLog.Add(
      new VariableOperation(OperationVerb.Delete, Environment.StackTrace));
  }

  public void Read()
  {
    AssertStateIs(VariableStates.Unknown, OperationVerb.Read);
    var value = Environment.GetEnvironmentVariable(Name, _target);
    if (value == null)
    {
      throw new UndefinedVariableException(Name);
    }
    Value = _parser(value).Just();
    _state = VariableStates.Defined;
    OperationsLog = OperationsLog.Add(
      new VariableOperation(OperationVerb.Read, Environment.StackTrace));
  }

  public void Define(T value)
  {
    AssertStateIsNot(VariableStates.Defined, OperationVerb.Define);
    Environment.SetEnvironmentVariable(Name, _deparser(value), _target);
    Value = value.Just();
    _state = VariableStates.Defined;
    OperationsLog = OperationsLog.Add(
      new VariableOperation(OperationVerb.Define, Environment.StackTrace));
  }

  public void ChangeValue(T value)
  {
    AssertStateIs(VariableStates.Defined, OperationVerb.Modify);
    Environment.SetEnvironmentVariable(Name, _deparser(value), _target);
    Value = value.Just();
    OperationsLog = OperationsLog.Add(
      new VariableOperation(OperationVerb.Modify, Environment.StackTrace));
  }

  private void AssertStateIs(VariableStates expectedState, OperationVerb operationVerb)
  {
    if (_state != expectedState)
    {
      throw new InvalidVariableStateException(operationVerb, _state, expectedState, OperationsLog); //bug
    }
  }

  private void AssertStateIsNot(VariableStates state, OperationVerb operationVerb)
  {
    if (_state == state)
    {
      //bug another exception
      throw new InvalidVariableStateException(operationVerb, _state, OperationsLog); //bug
    }
  }
}