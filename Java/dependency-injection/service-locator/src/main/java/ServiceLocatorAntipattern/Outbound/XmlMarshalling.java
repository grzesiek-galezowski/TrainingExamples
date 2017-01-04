package ServiceLocatorAntipattern.Outbound;

public class XmlMarshalling implements IMarshalling {
  public String of(String arg) {
    return "<" + arg + ">";
  }
}