using System.Linq;

namespace CourtesyImplementation._3_TellDontAsk
{
  public class Box : Node
  {
    private readonly Node[] _content;

    public Box(params Node[] content)
    {
      _content = content;
    }

    public void AddTo(ElephantCounter counter)
    {
      foreach (var node in _content)
      {
        node.AddTo(counter);
      }
    }
  }
}