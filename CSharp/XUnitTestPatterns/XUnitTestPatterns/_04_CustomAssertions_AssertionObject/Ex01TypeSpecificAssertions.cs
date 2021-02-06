using System.Collections.Generic;
using System.Runtime.Serialization;
using FluentAssertions;
using NUnit.Framework;

namespace XUnitTestPatterns._04_CustomAssertions_AssertionObject
{
  [DataContract]
  public class Ex01TypeSpecificAssertions
  {
    [Test]
    public void Trololololo()
    {
      3.Should().BeGreaterThan(2);

      "  ".Should().BeNullOrWhiteSpace();

      new List<int> { 1, 2, 3 }.Should().Contain(new List<int> { 1, 2 });

      GetType().Should().BeDecoratedWith<DataContractAttribute>();
    }
  }
}