using System.Threading.Channels;

namespace ActiveObjects;

public class ActiveObject : IAsyncDisposable
{
  private readonly Channel<object> _messageBox;
  private readonly Task _task;

  private ActiveObject(Channel<object> channel, Task task)
  {
    _messageBox = channel;
    _task = task;
  }

  public static ActiveObject CreateInstance(CancellationToken stoppingToken, IMessageService messageService)
  {
    var messageBox = Channel.CreateUnbounded<object>();
    return new ActiveObject(messageBox, Task.Run(async () =>
    {
      while (!stoppingToken.IsCancellationRequested && 
             !messageBox.Reader.Completion.IsCanceled &&
             !messageBox.Reader.Completion.IsCompleted &&
             !messageBox.Reader.Completion.IsCompletedSuccessfully)
      {
        try
        {
          var message = await messageBox.Reader.ReadAsync(stoppingToken).AsTask().WaitAsync(TimeSpan.FromSeconds(1), stoppingToken);
          await messageService.Handle(message);
        }
        catch (Exception e)
        {
          Console.WriteLine($"Could not publish message {e}");
        }
      }
    }, stoppingToken));
  }

  public async Task Send(object message)
  {
    await _messageBox.Writer.WriteAsync(message);
  }

  public async ValueTask DisposeAsync()
  {
    _messageBox.Writer.Complete();
    await _task;
  }
}