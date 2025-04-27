namespace ApplicationLogic.Ports;

public interface ITodoAppCommand
{
    Task Execute(CancellationToken cancellationToken);
}