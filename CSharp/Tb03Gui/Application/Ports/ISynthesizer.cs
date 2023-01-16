namespace Application.Ports;

public interface ISynthesizer
{
  void TurnOn();
  void SetBpm(double bpm);
  void Play(List<int> melody); //bug make this stronger type (maybe use Tb03Note or sth.)
}