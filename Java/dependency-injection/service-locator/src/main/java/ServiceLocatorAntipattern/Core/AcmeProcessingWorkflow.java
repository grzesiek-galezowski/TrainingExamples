
  public interface IProcessingWorkflow
  {
    void SetOutbound(IOutbound outbound);
    void ApplyTo(AcmeMessage message);
  }

  public class AcmeProcessingWorkflow : IProcessingWorkflow
  {
    private final IRepository _repository;
    private final IAuthorization _authorizationRules;
    private IOutbound _outbound;

    public AcmeProcessingWorkflow()
    {
      _authorizationRules = ApplicationRoot.Context.Resolve<IAuthorization>();
      _repository = ApplicationRoot.Context.Resolve<IRepository>();
    }

    public void SetOutbound(IOutbound outbound)
    {
      _outbound = outbound;
    }

    public void ApplyTo(AcmeMessage message)
    {
      message.AuthorizeUsing(_authorizationRules);
      _repository.Save(message);
      _outbound.Send(message);
    }
  }
