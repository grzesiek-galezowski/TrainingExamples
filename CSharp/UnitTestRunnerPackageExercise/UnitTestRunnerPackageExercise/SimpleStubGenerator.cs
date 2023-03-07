using Castle.DynamicProxy;

namespace UnitTestRunnerPackageExercise;

public static class SimpleStubGenerator
{
  private static readonly ProxyGenerator Generator = new();

  public static T Create<T>(object returnValue) where T : class
  {
    var interceptor = new FixedReturnValueInterceptor(returnValue);
    return Generator.CreateInterfaceProxyWithoutTarget<T>(interceptor);
  }

  private class FixedReturnValueInterceptor : IInterceptor
  {
    private readonly object _returnValue;

    public FixedReturnValueInterceptor(object returnValue)
    {
      _returnValue = returnValue;
    }

    public void Intercept(IInvocation invocation)
    {
      Console.WriteLine($"Intercepting method {invocation.Method.Name} with fixed return value {_returnValue}");
      invocation.ReturnValue = _returnValue;
    }
  }
}

