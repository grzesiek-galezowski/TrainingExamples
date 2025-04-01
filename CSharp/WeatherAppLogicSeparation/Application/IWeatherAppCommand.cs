namespace Application;

public interface IWeatherAppCommand
{
  Task Execute(CancellationToken token);
}