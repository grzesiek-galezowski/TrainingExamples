package com.github.grzesiek_galezowski.BastardInjection._4_BastardInjection.InMessages;

class StartMessage implements AcmeMessage {
  public void AuthorizeUsing(IAuthorization authorizationRules) {
    System.out.println("Authorizing Start with " + authorizationRules);
  }

  public void WriteTo(DataDestination dataDestination) {
    dataDestination.Add("Writing Start to " + dataDestination);
  }
}
