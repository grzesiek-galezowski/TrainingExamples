package com.github.grzesiek_galezowski.BastardInjection.Interfaces;

import com.github.grzesiek_galezowski.BastardInjection.Services.Authorization;

public interface Message {
  void authorizeUsing(Authorization authorizationRules);

  void writeTo(DataDestination dataDestination);
}
