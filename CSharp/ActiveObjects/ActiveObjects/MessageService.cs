namespace ActiveObjects;

public interface IMessageService
{
  public Task Handle(object message);
}