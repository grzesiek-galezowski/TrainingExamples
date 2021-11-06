using System.Collections.Generic;
using EnvVars;

namespace EnvVarsSpecification;

public class Examples
{
  [Test]
  public void ShouldDoManyThings()
  {
    var isOn = "IS_ON";
    var variable = UndefinedVariable<bool>
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

    var undefinedVariable = variable.Delete();
    Environment.GetEnvironmentVariable(isOn).Should().BeNull();
    EnvironmentVariablesNames().Should().NotContain(isOn);
    variable = undefinedVariable.Define(false);
    variable.Value().Should().Be(false);
  }

  [Test]
  public void ShouldDetectConflicts()
  {
    //GIVEN
    var undefinedVariable = UndefinedVariable<string>.ProcessWide("lol", s => s, s => s);
    var variable1 = undefinedVariable.Define("a");
    undefinedVariable.Invoking(v => v.Define("b"))
      .Should().ThrowExactly<InvalidVariableStateException>()
      .WithMessage("a");

    //WHEN

    //THEN
    Assert.Fail("unfinished");
  }

  private List<string> EnvironmentVariablesNames()
  {
    List<string> keys = new List<string>();
    foreach (var key in Environment.GetEnvironmentVariables().Keys)
    {
      keys.Add(key.ToString());
    }

    return keys;
  }
}