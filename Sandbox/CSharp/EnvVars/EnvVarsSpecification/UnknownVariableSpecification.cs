using EnvVars;
using TddXt.AnyRoot.Strings;

namespace EnvVarsSpecification;

public class UnknownVariableSpecification
{
  [Test]
  public void ShouldAllowAccessingItsName()
  {
    var name = Any.AlphaString();
    var variable = UnknownVariable<string>.ProcessWide(name, s => s, s => s);
    variable.Name.Should().Be(name);
  }

  [Test]
  public void ShouldThrowExceptionWhenTryingToReadEnvVariableDoesNotExist()
  {
    //GIVEN
    var name = Any.AlphaString();
    Environment.SetEnvironmentVariable(name, null);
    var variable = UnknownVariable<string>.ProcessWide(name, s => s, s => s);

    //WHEN - THEN
    variable.Invoking(v => v.Read()).Should().Throw<UndefinedVariableException>()
      .WithMessage($"*{name}*");
  }

  [Test]
  public void ShouldReturnDefinedVariableWithCorrectValueWhenReadingAnExistingVariable()
  {
    //GIVEN
    var name = Any.AlphaString();
    var value = Any.AlphaString();
    Environment.SetEnvironmentVariable(name, value);
    var variable = UnknownVariable<string>.ProcessWide(name, s => s, s => s);

    //WHEN
    DefinedVariable<string> definedVariable = variable.Read();

    //THEN
    definedVariable.Value().Should().Be(value);
  }

}