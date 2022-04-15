using Microsoft.Extensions.Logging.Debug;
using MockNoMock.UsersApp.Adapter.UserApi;

namespace MockNoMock.UsersApp.Adapter.Logging;

public class LoggingAdapter
{
  public LoggingAdapter()
  {
    UserApiSupport = new UserApiSupport(
      new LoggerFactory(new[]
      {
        new DebugLoggerProvider()
      }));
  }

  public IUserApiSupport UserApiSupport { get; }
}