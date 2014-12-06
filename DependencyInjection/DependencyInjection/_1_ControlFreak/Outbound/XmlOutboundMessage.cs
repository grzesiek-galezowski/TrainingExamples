using DependencyInjection._1_ControlFreak.Interfaces;

namespace DependencyInjection._1_ControlFreak.Outbound
{
  class XmlOutboundMessage : DataDestination
  {
    private readonly XmlMarshalling _xmlMarshalling;
    private string _content = string.Empty;

    public XmlOutboundMessage()
    {
      _xmlMarshalling = new XmlMarshalling();
    }

    public void SendVia(TcpSocket outputSocket)
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