namespace DealingWithNull.NullObject
{
  public interface CommandFactory
  {
    Command CreateOneDescribedBy(Frame frame);
  }

  public interface Command
  {
  }
}