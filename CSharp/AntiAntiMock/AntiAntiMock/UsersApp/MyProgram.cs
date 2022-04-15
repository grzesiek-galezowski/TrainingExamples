namespace MockNoMock.UsersApp;

public class MyProgram
{
  public MyProgram()
  {
    new CompositionRoot("http://something.com").NewUserController().Handle();
  }
}