using System.Collections.Generic;
using System.Linq;
using EnvVars;
using NullableReferenceTypesExtensions;

namespace EnvVarsSpecification;

public class Examples
{
  [Test]
  public void ShouldDoManyThings()
  {
    var isOn = "IS_ON";
    DefinedVariable<bool> variable = UndefinedVariable<bool>
      .ProcessWide(isOn, bool.Parse, b => b.ToString())
      .Define(true);
    variable.Value().Should().Be(true);
    Environment.GetEnvironmentVariable(isOn).Should().Be("True");
    
    variable = variable.ChangeValue(false);
    variable.Value().Should().Be(false);
    Environment.GetEnvironmentVariable(isOn).Should().Be("False");
    
    variable = variable.ChangeValue(true);
    variable.Value().Should().Be(true);
    Environment.GetEnvironmentVariable(isOn).Should().Be("True");

    UndefinedVariable<bool> undefinedVariable = variable.Delete();
    Environment.GetEnvironmentVariable(isOn).Should().BeNull();
    EnvironmentVariablesNames().Should().NotContain(isOn);
    variable = undefinedVariable.Define(false);
    variable.Value().Should().Be(false);
  }

  [Test]
  public void ShouldDetectConflictsStartingFromUndefinedVariable()
  {
    //GIVEN
    var undefinedVariable = UndefinedVariable<string>.ProcessWide("lol", s => s, s => s);
    var definedVariable = undefinedVariable.Define("a");
    undefinedVariable.Invoking(v => v.Define("b"))
      .Should().ThrowExactly<InvalidVariableStateException>()
      .WithMessage("You cannot Define a variable in a Defined state*" +
                   $"{OperationVerb.CreateContainer}*{OperationVerb.Define}*");

    //WHEN
    var newUndefinedVariable = definedVariable.Delete();
    definedVariable.Invoking(v => v.Delete())
      .Should().ThrowExactly<InvalidVariableStateException>()
      .WithMessage("Delete operation requires variable to be in state Defined, but was in Deleted." +
                   $"*{OperationVerb.CreateContainer}*{OperationVerb.Define}*{OperationVerb.Delete}*");
    definedVariable.Invoking(v => v.ChangeValue("b"))
      .Should().ThrowExactly<InvalidVariableStateException>()
      .WithMessage("Modify operation requires variable to be in state Defined, but was in Deleted." +
                   $"*{OperationVerb.CreateContainer}*{OperationVerb.Define}*{OperationVerb.Delete}*");
  }
  [Test]
  public void ShouldDetectConflictsStartingFromUnknownVariable()
  {
    //GIVEN
    Environment.SetEnvironmentVariable("lol", "a");
    var unknownVariable = UnknownVariable<string>.ProcessWide("lol", s => s, s => s);
    var definedVariable = unknownVariable.Read();
    unknownVariable.Invoking(v => v.Read())
      .Should().ThrowExactly<InvalidVariableStateException>()
      .WithMessage("Read operation requires variable to be in state Unknown, but was in Defined*" +
                   $"{OperationVerb.CreateContainer}*{OperationVerb.Read}*");

    //WHEN
    var newUndefinedVariable = definedVariable.Delete();
    definedVariable.Invoking(v => v.Delete())
      .Should().ThrowExactly<InvalidVariableStateException>()
      .WithMessage("Delete operation requires variable to be in state Defined, but was in Deleted." +
                   $"*| {OperationVerb.CreateContainer}*| {OperationVerb.Read}*| {OperationVerb.Delete}*");
    definedVariable.Invoking(v => v.ChangeValue("b"))
      .Should().ThrowExactly<InvalidVariableStateException>()
      .WithMessage("Modify operation requires variable to be in state Defined, but was in Deleted." +
                   $"*| {OperationVerb.CreateContainer}*| {OperationVerb.Read}*| {OperationVerb.Delete}*");
  }

  private static IEnumerable<string> EnvironmentVariablesNames()
  {
    return (from object? key in Environment.GetEnvironmentVariables().Keys select key.ToString().OrThrow()).ToList();
  }
}