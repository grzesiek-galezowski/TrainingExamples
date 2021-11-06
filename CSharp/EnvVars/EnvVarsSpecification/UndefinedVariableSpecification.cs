using EnvVars;
using TddXt.AnyRoot.Strings;

namespace EnvVarsSpecification;

public class UndefinedVariableSpecification
{
  [Test]
  public void ShouldAllowAccessingItsName()
  {
    var name = Any.AlphaString();
    var potentialVariable = UndefinedVariable<string>.ProcessWide(name, s=>s, s=>s);
    potentialVariable.Name.Should().Be(name);
  }

  [Test]
  public void ShouldSaveVariableWhenItIsDefined()
  {
    //GIVEN
    var name = Any.AlphaString();
    var value = Any.AlphaString();
    var potentialVariable = UndefinedVariable<string>.ProcessWide(name, s=>s, s=>s);
    
    //WHEN
    var storedVariable = potentialVariable.Define(value);

    //THEN
    Environment.GetEnvironmentVariable(name).Should().Be(value);
    storedVariable.Value().Should().Be(value);
  }
}