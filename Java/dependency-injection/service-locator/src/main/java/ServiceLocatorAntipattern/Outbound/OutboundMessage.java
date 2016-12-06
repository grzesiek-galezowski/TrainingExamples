
  public interface IOutboundMessage : DataDestination
  {
    void SendVia(IOutputSocket outputOutputSocket);
  }

  public class OutboundMessage : IOutboundMessage
  {
    private final IMarshalling _marshalling;
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