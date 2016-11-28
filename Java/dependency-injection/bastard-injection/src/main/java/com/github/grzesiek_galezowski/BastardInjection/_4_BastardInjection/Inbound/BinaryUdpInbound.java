
  interface IInbound : IDisposable //TODO error! forced by the UdpSocket
  {
    void SetDomainLogic(IProcessingWorkflow processingWorkflow);
    void StartListening();
  }

  class BinaryUdpInbound implements IInbound
  {
    private IProcessingWorkflow _processingWorkflow;
    private final IInputSocket _socket;
    private final IPacketParsing _parsing;

    public BinaryUdpInbound()
    {
      this(new UdpSocket(), new BinaryParsing());
    }

    //for tests
    public BinaryUdpInbound(IInputSocket socket, IPacketParsing parsing)
    {
      _socket = socket;
      _parsing = parsing;
    }

    public void SetDomainLogic(IProcessingWorkflow processingWorkflow)
    {
      _processingWorkflow = processingWorkflow;
    }
    
    public void StartListening()
    {
      byte[] frameData = new byte[100];
      while (_socket.Receive(frameData))
      {
        var message = _parsing.ResultFor(frameData);
        if (message != null)
        {
          if (_processingWorkflow != null)
          {
            _processingWorkflow.ApplyTo(message);
          }
        }
      }
    }

    public void Dispose()
    {
      _socket.Dispose(); //error!
    }
  }
