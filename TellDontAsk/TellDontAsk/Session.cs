using System;

namespace TellDontAsk
{
  public class Session
  {
    public string Owner { get; set; }
    public string Target { get; set; }
    public TimeSpan Duration { get; set; }
  }
}