using ActiveObjects;

public class SendToConsole : IMessageService
{
  public async Task Handle(object message)
  {
    Console.WriteLine($"Final object received {message}");
  }
}