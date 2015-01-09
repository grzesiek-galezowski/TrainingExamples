using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Interfaces;

namespace DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Outbound
{
  public interface IOutboundMessage
  {
    void SendVia(ISocket outputSocket);
  }

  public class XmlOutboundMessage : DataDestination, IOutboundMessage
  {
    private readonly XmlMarshalling _xmlMarshalling;
    private string _content = string.Empty;

    public XmlOutboundMessage(XmlMarshalling xmlMarshalling)
    {
      _xmlMarshalling = xmlMarshalling;
    }

    public void SendVia(ISocket outputSocket)
    {
      var marshalledContent = _xmlMarshalling.Of(_content);
      outputSocket.Open();
      outputSocket.Send(marshalledContent);
      outputSocket.Close();
    }

    public void Add(string s)
    {
      _content += s;
    }
  }
}