namespace CommandComparisonFactory
{
  class Program 
  {
    static void Main(string[] args)
    {
      var controller = new DispatcherBasedController(
        new ResultInProgressFactory(), 
        new AddUserCommandDispatcher(new Repository()));
      var result = controller.AddUser(new UserDto()
      {
        Name = "Johnny",
        Surname = "Bravo"
      });
    }
  }
}
