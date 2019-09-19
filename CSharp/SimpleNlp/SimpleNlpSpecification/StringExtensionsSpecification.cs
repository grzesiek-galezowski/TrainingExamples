using FluentAssertions;
using TddXt.SimpleNlp;
using Xunit;

namespace SimpleNlpSpecification
{
  public class StringExtensionsSpecification
  {
    [Fact]
    public void ShouldTokenizeStrings()
    {
      "a b c".SplitAndKeep("a").Should().BeEquivalentTo(new [] {"", "a", "b c"}, options => options.WithStrictOrdering());
      "a b c".SplitAndKeep("b").Should().BeEquivalentTo(new [] {"a", "b", "c"}, options => options.WithStrictOrdering());
      "a b c".SplitAndKeep("c").Should().BeEquivalentTo(new [] {"a b", "c", ""}, options => options.WithStrictOrdering());
      "a".SplitAndKeep("a").Should().BeEquivalentTo(new [] {"", "a", ""}, options => options.WithStrictOrdering());
      "abc".SplitAndKeep("a").Should().BeEquivalentTo(new [] {"abc"}, options => options.WithStrictOrdering());
      "abc".SplitAndKeep("b").Should().BeEquivalentTo(new [] {"abc"}, options => options.WithStrictOrdering());
      "abc".SplitAndKeep("c").Should().BeEquivalentTo(new [] {"abc"}, options => options.WithStrictOrdering());
      "".SplitAndKeep("").Should().BeEquivalentTo(new [] {"", "", ""}, options => options.WithStrictOrdering());
      "driver's license".SplitAndKeep("driver's").Should().BeEquivalentTo(new [] {"", "driver's", "license"}, options => options.WithStrictOrdering());
    }
  }
}
