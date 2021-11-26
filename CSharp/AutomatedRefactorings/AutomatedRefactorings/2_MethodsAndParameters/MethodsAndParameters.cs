using System;
using System.Globalization;
using System.Linq;

namespace AutomatedRefactorings._2_MethodsAndParameters
{
  //TODO encapsulate fields of friendly message (encapsulate fields)
  //TODO assume the FriendlyMessage type is third party. Wrap method return value, generate delegating members, move .Value to Send() call only - everything compiles
  //TODO deal with unclear responsibility in CreateFriendlyMessageFrom() (inline method)
  //TODO remove duplication of title casing (extract both methods, make one delegate to other, inline method)
  //TODO in this order, content, recipient and sender as parameters (introduce parameters in the ProcessInvitationMessage())
  //TODO move the send message inside our created MyMessage wrapper
  //TODO allow using different formattings in Send (extract method => introduce field => introduce parameter)
  //     optionally: get to Format(from, to, content), make method non static, extract again to make Format(message), non static again,
  //     optionally: extract class
  //     optionally: add new before, '()' after and quick fix all errors
  //TODO get rid of destination dependency from the MethodsAndParameters class
  //     and inline Send() method (introduce field, inline field, introduce parameter, inline method)
  //TODO rearrange ProcessInvitationMessage() signature parameters in from-to-what fashion

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
