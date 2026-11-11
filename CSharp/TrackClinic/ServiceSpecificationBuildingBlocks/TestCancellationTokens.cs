using System.Diagnostics;
using AwesomeAssertions.Extensions;

namespace ServiceSpecificationBuildingBlocks;

public static class TestCancellationTokens
{
  public static CancellationTokenSource CreateCancellationTokenSource() =>
    new(Debugger.IsAttached ? 2.Minutes() : 30.Seconds());
}