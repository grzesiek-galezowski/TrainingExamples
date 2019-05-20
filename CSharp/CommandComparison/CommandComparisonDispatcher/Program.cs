namespace CommandComparisonDispatcher
{
  class Program 
  {
    static void Main(string[] args)
    {
      var controller = new FactoryBasedController(new ResultInProgressFactory(), new CommandFactory(new Repository()));
      var result = controller.AddUser(new UserDto()
      {
        Name = "Johnny",
        Surname = "Bravo"
      });
    }
  }
}
