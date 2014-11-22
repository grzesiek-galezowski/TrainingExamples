using System;
using System.Globalization;
using System.Linq;

namespace AutomatedRefactorings.MethodsAndParameters
{
  //TODO encapsulate fields of message (encapsulate fields)
  //TODO deal with unclear responsibility in CreateFriendlyMessageFrom() (inline method)
  //TODO remove duplication of title casing (extract method)
  //TODO in this order, content, recipient and sender as parameters (introduce parameters)
  //TODO allow using different formattings (extract method => introduce field => introduce parameter)
  //TODO get rid of destination dependency and inline Send() method (introduce field, inline field, introduce parameter, inline method)
  //TODO rearrange ProcessInvitationMessage() parameters in from-to-what fashion

  public class MethodsAndParameters
  {
    private readonly MessageDestination _destination = new ConsoleDestination();

    public void ProcessInvitationMessage()
    {
      const string sender = "zenek";
      const string recipient = "jasiek";

      var message = CreateFriendlyMessageFrom(sender);

      message.To = Char.IsLower(recipient.First()) ?
        CultureInfo.CurrentCulture.TextInfo.ToTitleCase(recipient.ToLower()) : recipient;

      message.Content = "Hey, dude, let's hit the streets!";

      Send(message);

    }

    private FriendlyMessage CreateFriendlyMessageFrom(string sender)
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

    #region hidden code
    public void Main()
    {
      ProcessInvitationMessage();
    }
    #endregion

    public static void FitsSomewhereElse()
    {
      
    }
  }

  //TODO add example with if statement
}
