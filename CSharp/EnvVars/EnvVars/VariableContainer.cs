using System;
using Functional.Maybe;
using Functional.Maybe.Just;

namespace EnvVars;

public class VariableContainer<T>
{
  private readonly Func<T, string> _deparser;
  private readonly Func<string, T> _parser;
  private readonly EnvironmentVariableTarget _target;
  public Maybe<T> Value { get; private set; } = Maybe<T>.Nothing;
  public string Name { get; }

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
  }

  public void Delete()
  {
    Environment.SetEnvironmentVariable(Name, null, _target); //bug machine scope/user scope
    Value = Maybe<T>.Nothing;
  }

  public void Read()
  {
    var value = Environment.GetEnvironmentVariable(Name, _target);
    if (value == null)
    {
      throw new UndefinedVariableException(Name);
    }
    Value = _parser(value).Just();
  }

  public void Define(T value)
  {
    Environment.SetEnvironmentVariable(Name, _deparser(value), _target);
    Value = value.Just();
  }

  public void ChangeValue(T value)
  {
    Environment.SetEnvironmentVariable(Name, _deparser(value), _target); //bug machine/user scope
    Value = value.Just();
  }
}