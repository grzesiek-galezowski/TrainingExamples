namespace TelecomSystemNestedFunctions.Outbound
{
  public interface IOutboundMessageFactory
  {
    XmlOutboundMessage CreateOutboundMessage();
  }

  public class XmlOutboundMessageFactory : IOutboundMessageFactory
  {
    public XmlOutboundMessage CreateOutboundMessage()
    {
      return new XmlOutboundMessage(new XmlMarshalling());
    }
  }
}