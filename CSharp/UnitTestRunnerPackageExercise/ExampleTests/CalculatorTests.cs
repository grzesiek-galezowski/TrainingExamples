using UnitTestRunnerPackageExercise;

namespace ExampleTests;

public class CalculatorTests
{
  public void TestAdd()
  {
    // Arrange
    var mock = SimpleStubGenerator.Create<ICalculator>(returnValue: 42);

    // Act
    var result = mock.Add(1, 2);

    // Assert
    Assert.AreEqual(42, result);
  }
}