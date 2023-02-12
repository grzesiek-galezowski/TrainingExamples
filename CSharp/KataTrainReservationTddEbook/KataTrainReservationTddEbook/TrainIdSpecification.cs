﻿using System;
using TddXt.AnyRoot.Strings;
using TddXt.XFluentAssert.Api;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace KataTrainReservationTddEbook;

public class TrainIdSpecification
{
  [Fact]
  public void ShouldImplementEqualityCorrectly()
  {
    var str1 = Any.String();
    ObjectsOfType<TrainId>.ShouldHaveValueSemantics(
      new[]
      {
        () => new TrainId(str1),
      },
      new[]
      {
        () => new TrainId(str1 + " "),
      });
  }
}