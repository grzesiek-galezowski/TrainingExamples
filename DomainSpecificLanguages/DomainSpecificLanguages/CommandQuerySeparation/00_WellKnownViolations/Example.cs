using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandQuerySeparation._00_WellKnownViolations
{
  class Example
  {
    public void WellKnownViolations()
    {
      TraverseList();
      PopFromStack();
      ReadFromStream();
      WhatAboutRandomAndGuidsAndTimeAndAny();

    }

    private void WhatAboutRandomAndGuidsAndTimeAndAny()
    {
      var random = new Random(123);
      var number = random.Next();
      var guid = Guid.NewGuid();
      var time = DateTime.Now;
      //what about Any?
    }

    private static void ReadFromStream()
    {
      using (var stream = new MemoryStream())
      {
        byte[] buffer = new byte[2048]; // read in chunks of 2KB
        int bytesRead;
        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
          stream.Write(buffer, 0, bytesRead);
        }
        byte[] result = stream.ToArray();
        // TODO: do something with the result
      }
    }


    private static void PopFromStack()
    {
      Stack<int> stack = new Stack<int>();
      stack.Push(1);
      stack.Push(1);
      stack.Push(1);
      stack.Push(1);
      var lastIn = stack.Pop();
    }

    private static void TraverseList()
    {
      var l = new List<int> {1, 2, 3, 4};
      using (var enumerator = l.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Console.WriteLine(enumerator.Current);
        }
      }
    }
  }
}
