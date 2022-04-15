namespace MockNoMock.UsersApp.Adapter.UserApi;

public class DuplicateUserException : Exception
{
  public DuplicateUserException(Exception exception)
  : base("Attempting to create a user who already exists", exception)
  {
    
  }
}