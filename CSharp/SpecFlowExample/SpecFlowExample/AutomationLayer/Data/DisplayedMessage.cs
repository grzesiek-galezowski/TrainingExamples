namespace SpecFlowExample.AutomationLayer.Data
{
  public class DisplayedMessage
  {
    public string From { get; }
    public string To { get; }
    public string Text { get; }

    public DisplayedMessage(string from, string to, string text)
    {
      From = @from;
      To = to;
      Text = text;
    }
  }
}