using System;
using System.Globalization;
using System.Linq;

namespace AutomatedRefactorings.MethodsAndParameters
{
  //TODO encapsulate fields of message (encapsulate fields)
  //TODO deal with unclear responsibility in CreateFriendlyMessageFrom() (inline method)
  //TODO remove duplication of title casing (extract method)
  //TODO sender, recipient and content as parameters (introduce parameters)
  //TODO allow using different formattings (extract method => introduce field => introduce parameter)
  //TODO remove unnecessary Send

  public class MethodsAndParameters
  {
    private readonly IMessageDestination _destination = new ConsoleDestination();

    public void ProcessInvitationMessage()
    {
      const string sender = "zenek";
      const string recipient = "jasiek";

      var message = CreateFriendlyMessageFrom(sender);

      message.To = Char.IsLower(recipient.First()) ?
        CultureInfo.CurrentCulture.TextInfo.ToTitleCase(recipient.ToLower()) : recipient;

      message.Content = "Hey, dude, let's we hit the streets!";

      Send(message);

    }

    private static FriendlyMessage CreateFriendlyMessageFrom(string sender)
    {
      var message = new FriendlyMessage();

      message.From = Char.IsLower(sender.First())
                       ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(sender.ToLower())
                       : sender;
      return message;
    }

    private void Send(FriendlyMessage message)
    {
      _destination.Send("From: " + message.From + Environment.NewLine + 
        "To: " + message.To + Environment.NewLine +
        "Content: " + message.Content);
    }

    public void Main()
    {
      ProcessInvitationMessage();
    }
  }


  //TODO add example with if statement
}
