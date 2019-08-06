using System;

namespace PrivateMethodsAsEventHandlersRegisteredInMethod
{
  class Program
  {
    static void Main(string[] args)
    {
      var dictionary = new BiDirectionalDictionary(new ValueExpiryPolicy());
      dictionary.Add("A", "B");
      Console.WriteLine(dictionary.GetValueByKey("A"));
      Console.WriteLine(dictionary.GetKeyByValue("B"));
    }
  }
}
