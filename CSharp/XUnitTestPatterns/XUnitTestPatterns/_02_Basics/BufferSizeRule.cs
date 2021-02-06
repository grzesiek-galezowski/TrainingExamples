namespace XUnitTestPatterns._02_Basics
{
  public class BufferSizeRule
  {
    public bool CanHandle(string s)
    {
      return s != null && s.Length < 4;
    }
  }
}