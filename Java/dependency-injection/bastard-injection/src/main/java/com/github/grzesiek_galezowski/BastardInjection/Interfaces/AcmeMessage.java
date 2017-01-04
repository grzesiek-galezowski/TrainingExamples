package com.github.grzesiek_galezowski.BastardInjection.Interfaces;

import com.github.grzesiek_galezowski.BastardInjection.Services.IAuthorization;

public interface AcmeMessage {
  void authorizeUsing(IAuthorization authorizationRules);

  void writeTo(DataDestination dataDestination);
}
