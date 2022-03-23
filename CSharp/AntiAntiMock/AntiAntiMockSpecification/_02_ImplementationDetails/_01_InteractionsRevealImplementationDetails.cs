using System;
using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TddXt.AnyRoot;
using static TddXt.AnyRoot.Root;

namespace MockNoMockSpecification._02_ImplementationDetails
{
  internal class _01_InteractionsRevealImplementationDetails
  {
    [Test]
    public void ShouldInsertTextWithPrefixIntoCacheAndReturnItsId()
    {
      //GIVEN
      var dictionary = Substitute.For<IDictionary<Guid, IText>>();
      var textFactory = Substitute.For<ITextFactory>();
      var guidFactory = Substitute.For<IGuidFactory>();
      var textCache = new BrittleTextCache(dictionary, textFactory, guidFactory);
      var insertedText = Substitute.For<IText>();
      var prefixedText = Any.Instance<IText>();
      var prefix = Any.Instance<IText>();
      var generatedId = Any.Guid();

      textFactory.CreateText("[PREFIX]").Returns(prefix);
      insertedText.Prepend(prefix).Returns(prefixedText);
      guidFactory.GetNewGuid().Returns(generatedId);

      //WHEN
      var id = textCache.Insert(insertedText);

      //THEN
      id.Should().Be(generatedId);
      dictionary.Received(1).Add(id, prefixedText);
    }
  }

  public class BrittleTextCache
  {
    private readonly IDictionary<Guid, IText> _dictionary;
    private readonly ITextFactory _textFactory;
    private readonly IGuidFactory _guidFactory;

    public BrittleTextCache(
      IDictionary<Guid, IText> dictionary,
      ITextFactory textFactory,
      IGuidFactory guidFactory)
    {
      _dictionary = dictionary;
      _textFactory = textFactory;
      _guidFactory = guidFactory;
    }

    public Guid Insert(IText text)
    {
      var prefix = _textFactory.CreateText("[PREFIX]"); //could this be Create("[PRE").Append(Create("FIX])) ?
      var finalText = text.Prepend(prefix); //could this be Append()?
      var id = _guidFactory.GetNewGuid();
      _dictionary.Add(id, finalText);
      return id;
    }
  }

  public interface IGuidFactory
  {
    Guid GetNewGuid();
  }

  public interface ITextFactory
  {
    IText CreateText(string prefix);
  }

  public interface IText
  {
    IText Prepend(IText text);
    IText Append(IText text);
  }
}