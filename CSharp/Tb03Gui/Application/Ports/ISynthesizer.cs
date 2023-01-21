namespace Application.Ports;

public interface ISynthesizer
{
  void TurnOn();
  void SetBpm(double bpm);
  Task Play(List<int> melody, CancellationToken cancellationToken); //bug make this stronger type (maybe use Tb03Note or sth.)
}