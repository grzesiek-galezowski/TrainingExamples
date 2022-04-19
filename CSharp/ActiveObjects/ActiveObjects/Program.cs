using ActiveObjects;

var tokenSource = new CancellationTokenSource();
var ao1 = ActiveObject.CreateInstance(tokenSource.Token, new SendToConsole());
var ao2 = ActiveObject.CreateInstance(tokenSource.Token, new SendTo(ao1));

await ao2.Send("lol1");
await ao2.Send("lol2");
await ao2.Send("lol3");
await ao2.Send("lol4");

Console.WriteLine("Waiting for all messages to process");
await Task.Delay(TimeSpan.FromSeconds(5));

Console.WriteLine("Stopping active objects");
tokenSource.Cancel();

Console.WriteLine("Waiting for active objects to finish processing");
await Task.WhenAll(
  ao1.DisposeAsync().AsTask(), 
  ao2.DisposeAsync().AsTask());