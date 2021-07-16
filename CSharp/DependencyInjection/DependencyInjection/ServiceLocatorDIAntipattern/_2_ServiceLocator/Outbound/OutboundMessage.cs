using ServiceLocatorDIAntipattern._2_ServiceLocator.Interfaces;
using Unity;

namespace ServiceLocatorDIAntipattern._2_ServiceLocator.Outbound
{
  public interface IOutboundMessage : DataDestination
  {
    void SendVia(IOutputSocket outputOutputSocket);
  }

  class OutboundMessage : IOutboundMessage
  {
    private readonly IMarshalling _marshalling;
    private string _content = string.Empty;

    public OutboundMessage()
    {
      _marshalling = ApplicationRoot.Context.Resolve<IMarshalling>();
    }

    public void SendVia(IOutputSocket outputOutputSocket)
    {
      var marshalledContent = _marshalling.Of(_content);
      outputOutputSocket.Open();
      outputOutputSocket.Send(marshalledContent);
      outputOutputSocket.Close();
    }

    public void Add(string s)
    {
      _content += s;
    }
  }
}