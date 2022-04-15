using System.Threading.Tasks;
using MockNoMock.UsersApp.Adapter.UserApi;

namespace MockNoMockSpecification._07_IntegrationOrAdapterTests;

public class UserApiAdapterSpecification
{
  [Test]
  public async Task ShouldNotThrowWhenConfigServiceRespondsWith200OkToCreatingNewUser()
  {
    await using var driver = new UserApiAdapterDriver();
    var addedUser = new UserDto("Zenek", "Kopytko");

    driver.ConfigServiceResponds200OkToCreating(addedUser);

    //GIVEN - THEN
    await driver.Awaiting(d => d.CreateNewUser(addedUser)).Should().NotThrowAsync();
  }

  [Test]
  public async Task ShouldThrowAndLogErrorWhenConfigServiceRespondsWith409Conflict()
  {
    await using var driver = new UserApiAdapterDriver();
    var addedUser = new UserDto("Zenek", "Kopytko");

    driver.ConfigServiceResponds409ConflictToCreating(addedUser);

    //GIVEN - THEN
    await driver.Awaiting(d => d.CreateNewUser(addedUser)).Should().ThrowAsync<DuplicateUserException>();
    driver.LogsShouldContainErrorAboutDuplicateUser(addedUser);
  }
}