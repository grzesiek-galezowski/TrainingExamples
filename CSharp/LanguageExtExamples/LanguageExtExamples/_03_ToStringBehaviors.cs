using System.Collections.Immutable;
using FluentAssertions;
using LanguageExt;

namespace LanguageExtExamples;

public class _03_ToStringBehaviors
{
    [Test]
    public void ShouldHaveANiceStringRepresentation()
    {
        var d1 = new DataWithImmutableArray(ImmutableArray<int>.Empty.AddRange(1,2,3));
        d1.ToString().Should().Be("DataWithImmutableArray { Ints = System.Collections.Immutable.ImmutableArray`1[System.Int32] }");
        
        var d2 = new DataWithSeq(Seq<int>.Empty.Add(1).Add(2).Add(3));
        d2.ToString().Should().Be("DataWithSeq { Ints = [1, 2, 3] }");
        
        var d3 = new DataWithArr(Arr<int>.Empty.Add(1).Add(2).Add(3));
        d3.ToString().Should().Be("DataWithArr { Ints = [1, 2, 3] }");
    }

    public record DataWithImmutableArray(ImmutableArray<int> Ints);
    public record DataWithSeq(Seq<int> Ints);
    public record DataWithArr(Arr<int> Ints);
}