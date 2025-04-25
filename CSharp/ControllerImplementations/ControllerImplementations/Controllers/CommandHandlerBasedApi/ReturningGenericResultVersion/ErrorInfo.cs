using System;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion;

public class ErrorInfo(Exception e)
{
  public Exception E { get; } = e;
}