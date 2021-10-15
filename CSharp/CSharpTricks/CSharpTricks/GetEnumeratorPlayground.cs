using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using TddXt.AnyExtensibility;
using static TddXt.AnyRoot.Root;

namespace CSharpTricks;

public class GetEnumeratorPlayground
{
    [Test]
    public void Test1()
    {
        foreach (var number in 5)
        {
            Console.WriteLine(number);
        }

        Console.WriteLine();
        foreach (var number in 1..5)
        {
            Console.WriteLine(number);
        }

        Console.WriteLine();
        foreach (var number in (start: 1, end: 6, step: 2))
        {
            Console.WriteLine(number);
        }

        Console.WriteLine();
        foreach (var value in (Any, typeof(string), 2))
        {
            Console.WriteLine(value);
        }

        Console.WriteLine();
        foreach (var value in (Any, typeof(int), 2))
        {
            Console.WriteLine(value);
        }

        Console.WriteLine();
        foreach (var value in (3, RandomValues()))
        {
            Console.WriteLine(value);
        }

        //overengineering
        Console.WriteLine();
        foreach (var value in (start: 0, iterateWhile: LessThan(5), next: IncrementBy(1)))
        {
            Console.WriteLine(value);
        }

    }

    public static Func<int, bool> LessThan(int condition)
    {
        return num => num < condition;
    }

    public static Func<int, int> IncrementBy(int increment)
    {
        return num => num + increment;
    }

    private static Random RandomValues()
    {
        return new Random(Guid.NewGuid().GetHashCode());
    }
}

public static class EnumeratorExtensions
{
    public static IEnumerator GetEnumerator(this int num)
    {
        var enumerable = Enumerable.Range(0, num);
        return enumerable.GetEnumerator();
    }

    public static IEnumerator GetEnumerator(this Range r)
    {
        for(var i = r.Start.Value; i < r.End.Value ; i++)
        {
            yield return i;
        }
    }
    public static IEnumerator GetEnumerator(this (int start, int end, int step) v)
    {
        for(var i = v.start; i < v.end; i += v.step)
        {
            yield return i;
        }
    }
    
    public static IEnumerator GetEnumerator(this (BasicGenerator gen, Type t, int num) v)
    {
        for(var i = 0; i < v.num; i ++)
        {
            yield return v.gen.InstanceOf(new TypeBasedGenerator(v.t));
        }
    }
    
    public static IEnumerator GetEnumerator(this (int num, Random gen) v)
    {
        for(var i = 0; i < v.num; i ++)
        {
            yield return v.gen.Next();
        }
    }

    public static IEnumerator GetEnumerator(this (int start, Func<int, bool> keepIterating, Func<int, int> next) v)
    {
        for(var i = v.start; v.keepIterating(i); i = v.next(i))
        {
            yield return i;
        }
    }

}

public class TypeBasedGenerator : InlineGenerator<object>
{
    private readonly Type _vT;

    public TypeBasedGenerator(Type vT)
    {
        _vT = vT;
    }

    public object GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
    {
        return instanceGenerator.Instance(_vT, request);
    }
}