using System;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi
{
    public class ErrorInfo
    {
        public ErrorInfo(Exception e)
        {
            this.E = e;
        }

        public Exception E { get; }
    }
}