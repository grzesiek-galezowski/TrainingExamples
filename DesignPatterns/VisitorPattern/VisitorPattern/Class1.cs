using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPattern
{
  public interface Node
  {
    int GetElephantsCountInside();
  }

  public class Elephant : Node
  {
    public int GetElephantsCountInside()
    {
      return 1;
    }
  }

  public class Box : Node
  {
    private readonly Node[] _content;

    public Box(Node[] content)
    {
      _content = content;
    }

    public int GetElephantsCountInside()
    {
      return _content.Sum(b => b.GetElephantsCountInside());
    }
  }

}
