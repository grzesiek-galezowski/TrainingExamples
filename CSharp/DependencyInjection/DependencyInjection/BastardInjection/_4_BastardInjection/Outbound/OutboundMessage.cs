using BastardInjection._4_BastardInjection.Interfaces;

namespace BastardInjection._4_BastardInjection.Outbound
{
  public interface IOutboundMessage : DataDestination
  {
    void SendVia(IOutputSocket outputOutputSocket);
  }

  class OutboundMessage : IOutboundMessage
  {
    private readonly IMarshalling _marshalling;
    private string _content = string.Empty;

    public OutboundMessage() : this(new XmlMarshalling())
    {
      
    }

    public OutboundMessage(IMarshalling marshalling)
    {
      _marshalling = marshalling;
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