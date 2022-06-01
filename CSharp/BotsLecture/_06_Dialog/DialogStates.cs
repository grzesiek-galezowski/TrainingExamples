namespace _06_Dialog
{
  public static class DialogStates
  {
    public static IDialogState Initial => new InitialState();
    public static IDialogState WaitingForMissingData => new WaitingForMissingDataState();
  }
}