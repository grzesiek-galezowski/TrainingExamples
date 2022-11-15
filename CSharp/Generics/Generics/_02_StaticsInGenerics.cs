using FluentAssertions;

namespace Generics;

public class _02_StaticsInGenerics
{
  private readonly record struct MyMaybe<T>(T Value)
  {
    public static readonly MyMaybe<T> Nothing = new MyMaybe<T>();
  }

  [Test]
  public void Test1()
  {
    MyMaybe<int>.Nothing.Should().NotBe(MyMaybe<string>.Nothing);
  }

  ////////////////////////
 
  private class GlobalVariable<T>
  {
    public static T MutableValue = default!;
  }

  [Test]
  public void Test2()
  {
    GlobalVariable<int>.MutableValue = 123;
    GlobalVariable<string>.MutableValue = "abc";

    GlobalVariable<int>.MutableValue.Should().Be(123);
    GlobalVariable<string>.MutableValue.Should().Be("abc");
  }
}