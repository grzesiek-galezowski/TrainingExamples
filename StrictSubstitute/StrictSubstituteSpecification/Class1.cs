using System;
using System.Collections.Generic;
using System.Reflection;
using NSubstitute;
using NSubstitute.Core;

namespace StrictNSubstituteSpecification
{
    public static class StrictSubstitute
    {
      private static readonly Dictionary<object, List<Tuple<CallInfo, MethodInfo>>> _unwantedCalls = new Dictionary<object, List<Tuple<CallInfo, MethodInfo>>>();

      public static T For<T>() where T : class
      {
        var sub = Substitute.For<T>();
        MakeStrict(sub);

        return sub;
      }

      public static void MakeStrict<T>(T sub) where T : class
      {
        var methods = GetPublicInstanceMethods<T>();

        foreach (var methodInfo in methods)
        {
          Console.WriteLine(methodInfo.Name);
          var anyArgs = GenerateArgumentsFor(methodInfo);
          sub.WhenForAnyArgs(s => methodInfo.Invoke(s, anyArgs))
             .Do(ci =>
             {
               if (!_unwantedCalls.ContainsKey(sub)) _unwantedCalls[sub] = new List<Tuple<CallInfo, MethodInfo>>();
               _unwantedCalls[sub].Add(Tuple.Create(ci, methodInfo));
             });
        }
      }

      public static void ReceivedConfiguredCalls(this object substitute)
      {
        if (_unwantedCalls[substitute].Count > 0)
        {
          string result = "";
          foreach (var ci in _unwantedCalls[substitute])
          {
            result += ci.Item2 +" " + ArgumentValues(ci) + Environment.NewLine;
          }
          throw new Exception(result);
        }
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
        return typeof(T).GetMethods(BindingFlags.Instance | BindingFlags.Public);
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

  public class CallNotSpecifiedException : Exception
  {
    private readonly MethodInfo _methodInfo;

    public CallNotSpecifiedException(MethodInfo methodInfo)
      : base(methodInfo.ToString())
    {
      _methodInfo = methodInfo;
    }
  }
}
