using DependencyInjectionBefore._1_ControlFreak.Interfaces;

namespace DependencyInjectionBefore._1_ControlFreak.Outbound
{
  class XmlTcpOutbound
  {
    private readonly TcpSocket _outputSocket;

    public XmlTcpOutbound()
    {
      _outputSocket = new TcpSocket();
    }

    public void Send(AcmeMessage message)
    {
      var outboundMessage = new XmlOutboundMessage();
      message.WriteTo(outboundMessage);
      outboundMessage.SendVia(_outputSocket);
    }
  }
}