using ServiceLocatorDIAntipattern._2_ServiceLocator.Interfaces;
using Microsoft.Practices.Unity;

namespace ServiceLocatorDIAntipattern._2_ServiceLocator.Outbound
{
  public interface IOutbound
  {
    void Send(AcmeMessage message);
  }

  public class Outbound : IOutbound
  {
    private readonly IOutputSocket _outputOutputSocket;

    public Outbound()
    {
      _outputOutputSocket = ApplicationRoot.Context.Resolve<IOutputSocket>();
    }

    public void Send(AcmeMessage message)
    {
      var outboundMessage = new OutboundMessage();
      message.WriteTo(outboundMessage);
      outboundMessage.SendVia(_outputOutputSocket);
    }
  }
}