using BastardInjection._4_BastardInjection.Interfaces;

namespace BastardInjection._4_BastardInjection.Outbound
{
  public interface IOutbound
  {
    void Send(AcmeMessage message);
  }

  public class Outbound : IOutbound
  {
    private readonly IOutputSocket _outputSocket;

    public Outbound() : this(new TcpSocket())
    {
      
    }
    
    //for tests
    public Outbound(IOutputSocket outputSocket)
    {
      _outputSocket = outputSocket;
    }

    public void Send(AcmeMessage message)
    {
      var outboundMessage = new OutboundMessage();
      message.WriteTo(outboundMessage);
      outboundMessage.SendVia(_outputSocket);
    }
  }
}