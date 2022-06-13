public interface IContainable
{
  //Propose method inherited by both Elephant
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

public class Tests
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
