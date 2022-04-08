namespace MockNoMock;

public class Broadcast
{
  private readonly IRecipient _mock1;
  private readonly IRecipient _mock2;

  public Broadcast(IRecipient mock1, IRecipient mock2)
  {
    _mock1 = mock1;
    _mock2 = mock2;
  }

  public void MakeFor(Work work)
  {
    _mock1.Handle(work);
    _mock2.Handle(work);
  }
}