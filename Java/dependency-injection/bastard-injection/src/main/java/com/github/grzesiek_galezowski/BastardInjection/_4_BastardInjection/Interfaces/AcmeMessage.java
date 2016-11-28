package com.github.grzesiek_galezowski.BastardInjection._4_BastardInjection.Interfaces;

public interface AcmeMessage {
  void AuthorizeUsing(IAuthorization authorizationRules);

  void WriteTo(DataDestination dataDestination);
}
