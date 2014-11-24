using System;

namespace DealingWithNull.Maybe
{
  //TODO three methods executing cache and reacting differently to null value
  public class Problem
  {
    public void Execute()
    {
      var cache = new Cache();
      
      __1(cache);
      __2(cache);
      __3(cache);
    }

    private static void __1(Cache cache)
    {
      //1
      var value = Get(cache, 123, new RealLog());
      if (value != null)
      {
        Console.WriteLine("Got: " + value + " of length " + value.Length);
      }
    }

    private static void __2(Cache cache)
    {
      //2
      AddTo(cache, 333);
    }

    private static void __3(Cache cache)
    {
      //3
      var value2 = FindFirstOf(new[] {33, 44, 66}, cache);
      if (value2 != null)
      {
        Console.WriteLine("Doing something more with found value of length: " + value2.Length);
      }
    }



    private static string Get(Cache cache, int deviceId1, Log log)
    {
      var result = cache.Retrieve(deviceId1);
      if (result != null)
      {
        log.Write("Item found : " + result + " with length " + result.Length);
      }
      return result;
    }

    private static void AddTo(Cache cache, int deviceId2)
    {
      //assuming adding is costly
      var result = cache.Retrieve(deviceId2);
      if (result == null)
      {
        cache.Insert(deviceId2);
      }
    }

    private static string FindFirstOf(int[] ids, Cache cache)
    {
      foreach (var id in ids)
      {
        var value = cache.Retrieve(id);
        if (value != null)
        {
          return value;
        }
      }

      return null;
    }



  }

  public class RealLog : Log
  {
    public void Write(string message)
    {
      
    }
  }

  internal interface Log
  {
    void Write(string message);
  }

  public class Cache
  {
    public string Retrieve(int deviceId1)
    {
      return null;
    }

    public void Insert(int deviceId2)
    {
      
    }
  }
}
