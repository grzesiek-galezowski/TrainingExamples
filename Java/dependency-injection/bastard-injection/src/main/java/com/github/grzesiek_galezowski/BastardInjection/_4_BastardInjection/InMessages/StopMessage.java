package com.github.grzesiek_galezowski.BastardInjection._4_BastardInjection.InMessages;

class StopMessage implements AcmeMessage {
  public void AuthorizeUsing(IAuthorization authorizationRules) {
    System.out.println("Authorizing Stop with " + authorizationRules);
  }

  public void WriteTo(DataDestination dataDestination) {
    System.out.println("Writing Stop to " + dataDestination);
  }
}