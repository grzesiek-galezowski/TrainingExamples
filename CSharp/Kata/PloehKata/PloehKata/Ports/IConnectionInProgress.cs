namespace PloehKata.Ports
{
  public interface IConnectionInProgress
  {
    void UserNotFound();
    void InvalidUserId();
    void InvalidOtherUserId();
    void Success(UserDto userDto);
    void OtherUserNotFound();
  }
}