namespace ServiceLocatorDIAntipattern._2_ServiceLocator.Outbound
{
  internal interface IMarshalling
  {
    string Of(string arg);
  }

  class XmlMarshalling : IMarshalling
  {
    public string Of(string arg)
    {
      return "<" + arg + ">";
    }
  }
}