using UnitTestRunnerPackageExercise;

namespace ExampleTests;

public class UserTests
{
  public void TestUserProperties()
  {
    // Arrange
    var testDataGenerator = new TestDataGenerator();
    var user = new User();

    var firstName = testDataGenerator.GenerateString(10);
    var lastName = testDataGenerator.GenerateString(10);
    var age = testDataGenerator.GenerateInt();

    // Act
    user.FirstName = firstName;
    user.LastName = lastName;
    user.Age = age;

    // Assert
    Assert.AreEqual(firstName, user.FirstName);
    Assert.AreEqual(lastName, user.LastName);
    Assert.AreEqual(age, user.Age);
  }
}

internal class User
{
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public int Age { get; set; }
}