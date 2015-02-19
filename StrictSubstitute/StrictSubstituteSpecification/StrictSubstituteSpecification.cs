using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using Telerik.JustMock;
using Telerik.JustMock.Expectations;

namespace StrictNSubstituteSpecification
{
  public class StrictSubstituteSpecification
  {
    private Context _context;

    [SetUp]
    public void SetContext()
    {
      _context = new Context();
    }

    [Test]
    public void ShouldAllowOverridingExceptionsWithNSubstituteSyntax()
    {
      //GIVEN
      var mock = _context.Mock<ITested>();
      var mock2 = _context.Mock<ITested>();

      _context.Expect(() => mock.VoidMethod(1, 2, "3")).InOrder();
      _context.Expect(() => mock.VoidMethod(1, 2, "4")).InOrder();
      _context.Expect(() => mock.VoidMethod(1, 2, "5")).InOrder();
      _context.Expect(() => mock2.VoidMethod(1, 2, "3")).InOrder();
      _context.Expect(() => mock2.VoidMethod(1, 2, "4")).InOrder();
      _context.Expect(() => mock2.VoidMethod(1, 2, "5")).InOrder();
      _context.Allow(() => mock.MethodWithReturn(1, 2, "7")).Returns("12");


      mock.VoidMethod(1, 2, "3");
      mock.VoidMethod(1, 2, "4");
      mock.VoidMethod(1, 2, "5");
      mock2.VoidMethod(1, 2, "3");
      mock2.VoidMethod(1, 2, "4");
      mock2.VoidMethod(1, 2, "5");

      Assert.AreEqual("12", mock.MethodWithReturn(1, 2, "7"));
      _context.AssertIsSatisfied();
    }
  }


  public class Context
  {
    private readonly List<object> _mocks = new List<object>();

    public T Mock<T>()
    {
      var mock = Telerik.JustMock.Mock.Create<T>(Behavior.Strict);
      _mocks.Add(mock);
      return mock;
    }

    public ActionExpectation Expect(Expression<Action> expression)
    {
      return Telerik.JustMock.Mock.Arrange(expression);
    }

    public ActionExpectation Allow(Expression<Action> expression)
    {
      var expectation = Telerik.JustMock.Mock.Arrange(expression);
      expectation.OccursAtLeast(0);
      return expectation;
    }

    public FuncExpectation<T> Allow<T>(Expression<System.Func<T>> expression)
    {
      var expectation = Telerik.JustMock.Mock.Arrange(expression);
      expectation.OccursAtLeast(0);
      return expectation;
    }


    public void AssertIsSatisfied()
    {
      foreach (var mock in _mocks)
      {
        Telerik.JustMock.Mock.AssertAll(mock);
      }
    }
  }

  public interface ITested
  {
    void VoidMethod(int a, int b, string c);
    string MethodWithReturn(int a, int b, string c);
    int Prop { get; set; }
    event Action<int> MyEvent;
  }

  public interface IGenericTested
  {
    //bug
  }
}
