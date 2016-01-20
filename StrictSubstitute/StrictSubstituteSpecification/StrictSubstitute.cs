using System;
using System.Collections.Generic;
using System.Reflection;
using NSubstitute;
using NSubstitute.Core;
using NSubstitute.Core.Arguments;
using NUnit.Framework;
using TddEbook.TddToolkit;

namespace StrictNSubstituteSpecification
{
  public class StrictSubstitute
  {

    public T For<T>(Action<T> specifyCalls) where T : class
    {
      var sub = Substitute.For<T>();
      var methods = GetPublicInstanceMethods<T>();
      Dictionary<MethodInfo, MethodReturnValue> results = new Dictionary<MethodInfo, MethodReturnValue>();


      foreach (var methodInfo in methods)
      {
        var anyArgs = GenerateArgumentsFor(methodInfo);


        var returnType = methodInfo.ReturnType;
        if (returnType != typeof (void))
        {
          FuncBasedReturnValue returnForMethod = new FuncBasedReturnValue(returnType);

          methodInfo.Invoke(sub, anyArgs)
            .ReturnsForAnyArgs(x => returnForMethod.Get());
          results[methodInfo] = returnForMethod;
        }
        else
        {
          ActionBasedReturnValue voidReturnForMethod = new ActionBasedReturnValue();
          sub.WhenForAnyArgs(s => methodInfo.Invoke(s, anyArgs))
             .Do(ci =>
             {
               voidReturnForMethod.Invoke();
             });

          results[methodInfo] = voidReturnForMethod;
        }

      }
      specifyCalls(sub);

      foreach (var methodReturnValue in results.Values)
      {
        methodReturnValue.StartThrowingExceptions();
      }

      return sub;
    }


    private static string ArgumentValues(Tuple<CallInfo, MethodInfo> ci)
    {
      string result = "";
      foreach (var arg in ci.Item1.Args())
      {
        result += "[" + arg + "]";
      }
      return result;
    }

    private static MethodInfo[] GetPublicInstanceMethods<T>() where T : class
    {
      return typeof (T).GetMethods(BindingFlags.Instance | BindingFlags.Public);
    }

    private static object[] GenerateArgumentsFor(MethodInfo method)
    {
      var parameters = new List<object>();
      foreach (var parameter in method.GetParameters())
      {
        parameters.Add(GetDefault(parameter.ParameterType));
      }
      return parameters.ToArray();
    }

    public static object GetDefault(Type type)
    {
      if (type.IsValueType)
      {
        return Activator.CreateInstance(type);
      }
      return null;
    }
  }

  public class ActionBasedReturnValue : MethodReturnValue
  {
    private bool _throw = false;

    public void StartThrowingExceptions()
    {
      _throw = true;
    }

    public void Invoke()
    {
      if (_throw)
      {
        throw new AccessViolationException("lolek2");
      }
    }
  }

  public class FuncBasedReturnValue : MethodReturnValue
  {
    private readonly Type _returnType;
    private bool _throw = false;

    public FuncBasedReturnValue(Type returnType)
    {
      _returnType = returnType;
    }

    public object Get()
    {
      if(!_throw)
        return StrictSubstitute.GetDefault(_returnType);
      else
        throw new AccessViolationException("lolokimono");
    }

    public void StartThrowingExceptions()
    {
      _throw = true;
    }
  }

  public class CallNotSpecifiedException : Exception
  {
    private readonly MethodInfo _methodInfo;

    public CallNotSpecifiedException(MethodInfo methodInfo)
      : base(methodInfo.ToString())
    {
      _methodInfo = methodInfo;
    }
  }

  public class Lolki
  {
    [Test]
    public void ShouldAllowOverridingExceptionsWithNSubstituteSyntax()
    {
      //GIVEN
      var sub = new StrictSubstitute().For<ITested>(s =>
      {
        s.MethodWithReturn(1, 2, "a").Returns("123");
        s.When(_ => _.VoidMethod(1, 2, "2")).Do(c => { });
      });

      var x = sub.MethodWithReturn(1, 2, "a");
      Assert.AreEqual("123", x);
      Assert.Throws<AccessViolationException>(() => x = sub.MethodWithReturn(1, 4, "a"));

      sub.VoidMethod(1,2,"2");
      Assert.Throws<AccessViolationException>(() => sub.VoidMethod(1,2,"3"));
    }

    [Test]
    public void ShouldAllowOverridingExceptionsWithNSubstituteSyntax2()
    {
      //GIVEN
      var sub = Substitute.For<ITested>();
      
      Action a = new Action(() => { });

      sub.When(m => m.VoidMethod(Arg.Any<int>(), Arg.Any<int>(),Arg.Any<string>()))
        .Do(c => a());
      sub.When(m => m.VoidMethod(1,2,"")).Do(c => { });

      a = new Action(() => { throw new Exception();});

      sub.VoidMethod(1,2,"");
    }
  }
}

