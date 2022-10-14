using FluentAssertions;

namespace Generics;

public class _01_SimpleGenericClass
{
  public class GenericObject<T>
  {
    public T Value { get; }

    public GenericObject(T value)
    {
      Value = value;
    }
  }

  [Test]
  public void X()
  {
    new GenericObject<int>(123).Value.Should().Be(123);
  }

  
}