using System;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultVersion;

public class ErrorInfo(Exception e)
{
  public Exception E { get; } = e;
}