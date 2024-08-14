namespace RadicalCqsWebApp;

public interface IResultConsumer<in TSuccess, in TError>
{
  void Success(TSuccess success);
  void Error(TError error);
}