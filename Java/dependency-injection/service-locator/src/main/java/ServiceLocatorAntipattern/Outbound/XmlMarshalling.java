
public interface IMarshalling
  {
    string Of(string arg);
  }

public class XmlMarshalling : IMarshalling
  {
    public string Of(string arg)
    {
      return "<" + arg + ">";
    }
  }
}