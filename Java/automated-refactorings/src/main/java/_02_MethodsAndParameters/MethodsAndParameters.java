package _02_MethodsAndParameters;

//TODO encapsulate fields of message (encapsulate fields)
//TODO assume the encapsulated type is third party. Copy type -> generate delegating members
//TODO deal with unclear responsibility in CreateFriendlyMessageFrom() (inline method)
//TODO remove duplication of title casing (extract both methods, make one delegate to other, inline method)
//TODO in this order, content, recipient and sender as parameters (introduce parameters)
//TODO allow using different formattings in Send (extract method => introduce field => introduce parameter)
//     optionally: get to Format(from, to, content), make method non static, extract again to make Format(message), non static again, extract class
//TODO get rid of destination dependency and inline Send() method (introduce field, inline field, introduce parameter, inline method)
//TODO rearrange ProcessInvitationMessage() signature parameters in from-to-what fashion

import org.apache.commons.lang3.StringUtils;

public class MethodsAndParameters {
  private final MessageDestination destination = new ConsoleDestination();

  public void processInvitationMessage() {
    final String sender = "zenek";
    final String recipient = "jasiek";

    FriendlyMessage message = createFriendlyMessageFrom(sender);

    message.To = Character.isLowerCase(recipient.charAt(0)) ?
        StringUtils.capitalize(recipient.toLowerCase()) : recipient;

    message.Content = "Hey, dude, let's hit the streets!";

    send(message);

  }

  private FriendlyMessage createFriendlyMessageFrom(String sender) {
    final FriendlyMessage message = new FriendlyMessage();

    message.From = Character.isLowerCase(sender.charAt(0))
        ? StringUtils.capitalize(sender.toLowerCase())
        : sender;
    return message;
  }

  private void send(FriendlyMessage message) {
    destination.send("From: " + message.From + System.lineSeparator() +
        "To: " + message.To + System.lineSeparator() +
        "Content: " + message.Content);
  }

  public void main() {
    processInvitationMessage();
  }

  public static void fitsSomewhereElse() {

  }
}

//TODO add example with if statement
