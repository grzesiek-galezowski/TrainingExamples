using ConformingContainerAntipattern._3_ConformingContainer.Interfaces;

namespace ConformingContainerAntipattern._3_ConformingContainer.Outbound
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
      var outboundMessage = ApplicationRoot.Context.Resolve<IOutboundMessage>();
      message.WriteTo(outboundMessage);
      outboundMessage.SendVia(_outputOutputSocket);
    }
  }
}