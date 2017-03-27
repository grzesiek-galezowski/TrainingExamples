package ServiceLocatorAntipattern.Outbound;

public class XmlMarshalling implements Marshalling {
  public String of(String arg) {
    return "<" + arg + ">";
  }
}