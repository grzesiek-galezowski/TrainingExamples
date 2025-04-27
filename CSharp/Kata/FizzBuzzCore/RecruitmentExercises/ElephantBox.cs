using System.Collections.Generic;
using NUnit.Framework;

namespace RecruitmentExercises;

public interface IContainable
{
  //Propose a method inherited by both Elephant
  //and Box that will allow getting the elephant count
}

class Elephant : IContainable
{
}

class Box : IContainable
{
  private readonly IReadOnlyList<IContainable> _content;

  public Box(params IContainable[] content)
  {
    _content = content;
  }
}

public class ElephantBoxExamples
{
  [Test]
  public void Example()
  {
    //Elephant count should be 3
    IContainable box = new Box(
      new Box(
        new Elephant()),
      new Box(
        new Box(
          new Elephant(),
          new Box(
            new Elephant()))));
  }
}
