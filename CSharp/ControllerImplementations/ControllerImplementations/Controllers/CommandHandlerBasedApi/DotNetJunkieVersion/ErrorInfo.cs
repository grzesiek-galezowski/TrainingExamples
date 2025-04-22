using System;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion;

public class ErrorInfo(Exception e)
{
  public Exception E { get; } = e;
}