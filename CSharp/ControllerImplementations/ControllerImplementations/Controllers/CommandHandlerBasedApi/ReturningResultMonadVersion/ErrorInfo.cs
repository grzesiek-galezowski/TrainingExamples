using System;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultMonadVersion;

public class ErrorInfo(Exception e)
{
  public Exception E { get; } = e;
}