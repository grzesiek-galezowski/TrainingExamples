using System.Threading.Tasks;

namespace MockNoMockSpecification._07_IntegrationOrAdapterTests;

internal interface IHttpBasedUserApi
{
  Task CreateNewUser(UserDto userDto);
}