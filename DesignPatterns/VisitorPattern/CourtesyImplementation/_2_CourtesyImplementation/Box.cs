using System.Linq;

namespace CourtesyImplementation._2_CourtesyImplementation
{
  public class Box : Node
  {
    private readonly Node[] _content;

    public Box(params Node[] content)
    {
      _content = content;
    }

    public int GetElephantsCountInside()
    {
      return _content.Sum(b => b.GetElephantsCountInside());
    }
  }
}