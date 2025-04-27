namespace ActiveObjects;

public class SendTo : IMessageService
{
  private readonly ActiveObject _ao1;

  public SendTo(ActiveObject ao1)
  {
    _ao1 = ao1;
  }

  public async Task Handle(object message)
  {
    Console.WriteLine($"Sending {message} to next object");
    await _ao1.Send(message);
  }
}