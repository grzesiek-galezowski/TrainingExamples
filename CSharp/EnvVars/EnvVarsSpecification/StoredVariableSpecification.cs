using EnvVars;
using TddXt.AnyRoot.Strings;

namespace EnvVarsSpecification;

public class StoredVariableSpecification
{
  [Test]
  public void ShouldAllowAccessingAssignedValue()
  {
    //GIVEN
    var name = Any.AlphaString();
    var value = Any.AlphaString();
    Environment.SetEnvironmentVariable(name, value);
    
    //WHEN
    var storedVariable = new StoredVariable<string>(
      TestVariableContainers.VariableContainerWithValue(name));

    //THEN
    storedVariable.Name.Should().Be(name);
    storedVariable.Value().Should().Be(value);
  }
  
  [Test]
  public void ShouldAllowDeletingTheVariable()
  {
    //GIVEN
    var name = Any.AlphaString();
    var value = Any.AlphaString();
    Environment.SetEnvironmentVariable(name, value);
    var storedVariable = new StoredVariable<string>(
      TestVariableContainers.VariableContainerWithValue(name));
    
    //WHEN
    var potentialVariable = storedVariable.Delete();

    //THEN
    potentialVariable.Name.Should().Be(name);
    Environment.GetEnvironmentVariable(name).Should().BeNull();
  }

  [Test]
  public void ShouldAllowRedefiningItsValue()
  {
    //GIVEN
    var name = Any.AlphaString();
    var value = Any.AlphaString();
    var newValue = Any.AlphaString();
    Environment.SetEnvironmentVariable(name, value);
    var storedVariable = new StoredVariable<string>(
      TestVariableContainers.VariableContainerWithValue(name));
    
    //WHEN
    var newVar = storedVariable.ChangeValue(newValue);
  
    //THEN
    newVar.Value().Should().Be(newValue);
    Environment.GetEnvironmentVariable(name).Should().Be(newValue);
  }
}