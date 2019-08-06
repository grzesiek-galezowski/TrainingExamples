using System;

namespace PrivateMethodsAsEventHandlers
{
  class Program
  {
    static void Main(string[] args)
    {
      var dictionary = new BiDirectionalDictionary(new ExpiryPolicy());
      dictionary.Add("A", "B");
      Console.WriteLine(dictionary.GetValueByKey("A"));
      Console.WriteLine(dictionary.GetKeyByValue("B"));
    }
  }
}
