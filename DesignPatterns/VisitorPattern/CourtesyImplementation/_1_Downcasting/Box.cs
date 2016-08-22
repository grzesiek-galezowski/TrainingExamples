namespace CourtesyImplementation._1_Downcasting
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
      int count = 0;
      foreach (var node in _content)
      {
        if (node is Box)
        {
          count += ((Box) node).GetElephantsCountInside();
        }
        else if(node is Elephant)
        {
          count += 1;
        }
      }
      return count;
    }
  }
}