using System.Collections.Immutable;
using FluentAssertions;
using LanguageExt;

namespace LanguageExtExamples;

public class _04_EqualityBehaviors
{
    [Test]
    public void ShouldHaveCompareCollectionsByValue()
    {
        var d11 = new DataWithImmutableArray(ImmutableArray<int>.Empty.AddRange(1, 2, 3));
        var d12 = new DataWithImmutableArray(ImmutableArray<int>.Empty.AddRange(1, 2, 3));
        d11.Should().NotBe(d12);

        var d111 = new DataWithImmutableList(ImmutableList<int>.Empty.Add(1).Add(2).Add(3));
        var d112 = new DataWithImmutableList(ImmutableList<int>.Empty.Add(1).Add(2).Add(3));
        d111.Should().NotBe(d112);

        var d21 = new DataWithSeq(Seq<int>.Empty.Add(1).Add(2).Add(3));
        var d22 = new DataWithSeq(Seq<int>.Empty.Add(1).Add(2).Add(3));
        d21.Should().Be(d22); //look at performance (the yellow NCrunch dot)
        
        var d31 = new DataWithArr(Arr<int>.Empty.Add(1).Add(2).Add(3));
        var d32 = new DataWithArr(Arr<int>.Empty.Add(1).Add(2).Add(3));
        d31.Should().Be(d32); //look at performance (the yellow NCrunch dot)
    }

    public record DataWithImmutableArray(ImmutableArray<int> Ints);
    public record DataWithImmutableList(ImmutableList<int> Ints);
    public record DataWithSeq(Seq<int> Ints);
    public record DataWithArr(Arr<int> Ints);
}