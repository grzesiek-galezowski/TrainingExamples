namespace DotNetJunkieKata
{
  public class MoveCustomerCommandHandler : ICommandHandler<MoveCustomerCommand>
  {
    private readonly UnitOfWork db;

    public MoveCustomerCommandHandler(
      UnitOfWork db,
      OtherDependencies dep)
    {
      this.db = db;
    }

    public virtual void Handle(MoveCustomerCommand command)
    {
      // TODO: Logic here
    }
  }
}