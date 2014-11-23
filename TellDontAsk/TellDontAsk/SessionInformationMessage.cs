using System;
using System.Runtime.Serialization;

namespace TellDontAsk
{
  [DataContract]
  public class SessionInformationMessage
  {
    [DataMember]
    public string Owner { get; set; }
    [DataMember]
    public string Target { get; set; }
    [DataMember]
    public TimeSpan Duration { get; set; }
  }
}