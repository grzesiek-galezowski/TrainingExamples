namespace DealingWithNull.Exceptions
{
  class Problem
  {
    public void Main()
    {
      var connectionFactory = new ConnectionFactory();

      var connection = connectionFactory.OpenConnectionTo("http://hardcoded_always_valid_uri.com");

      if (connection != null) //success
      {
        connection.Send("Hello World");
      }
    }
  }
}
