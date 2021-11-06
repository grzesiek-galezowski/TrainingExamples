using EnvVars;
using TddXt.AnyRoot.Strings;

namespace EnvVarsSpecification;

public class UninitializedVariableSpecification
{
  [Test]
  public void ShouldAllowAccessingItsName()
  {
    var name = Any.AlphaString();
    var variable = new UninitializedVariable<string>(name, s => s, s => s);
    variable.Name.Should().Be(name);
  }

  [Test]
  public void ShouldThrowExceptionWhenTryingToReadEnvVariableDoesNotExist()
  {
    //GIVEN
    var name = Any.AlphaString();
    var variable = new UninitializedVariable<string>(name, s => s, s => s);

    //WHEN - THEN
    variable.Invoking(v => v.Read()).Should().Throw<UndefinedVariableException>()
      .WithMessage($"*{name}*");
  }

  [Test]
  public void ShouldReturnStoredVariableWithCorrectValueWhenReadingAnExistingVariable()
  {
    //GIVEN
    var name = Any.AlphaString();
    var value = Any.AlphaString();
    Environment.SetEnvironmentVariable(name, value);
    var variable = new UninitializedVariable<string>(name, s => s, s => s);

    //WHEN
    StoredVariable<string> storedVariable = variable.Read();

    //THEN
    storedVariable.Value().Should().Be(value);
  }

}