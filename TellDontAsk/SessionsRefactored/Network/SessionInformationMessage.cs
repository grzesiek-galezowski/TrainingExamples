using System;
using System.Runtime.Serialization;

namespace SessionsRefactored.Network
{
  [DataContract]
  public class SessionInformationMessage
  {
    [DataMember]
    public string Owner { get; set; }
    [DataMember]
    public string Target { get; set; }
    [DataMember]
    public int Id { get; set; }
  }
}