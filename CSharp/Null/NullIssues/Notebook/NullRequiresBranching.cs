using System;
using System.ComponentModel.Design.Serialization;
using NUnit.Framework;

namespace Notebook;

public class NullRequiresBranching
{
  [Test]
  public void Trolololol()
  {
    //let's assume this is complex flow and we want to be completely safe
    //therefore we need to check all references (also in functions)
    
    var a = GetA();
    var b = GetB(a);
    var c = GetC(b);
    var d = GetD(c);
    Console.WriteLine(d);
  }

  private string GetB(string arg)
  {
    return arg + GetA();
  }
  
  private string GetC(string arg)
  {
    return arg + GetB(arg);
  }
  
  private string GetD(string arg)
  {
    return arg + GetC(arg);
  }

  private string GetA()
  {
    return "";
  }
}