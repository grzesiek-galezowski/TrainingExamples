package com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Services.Authorization;

public interface AcmeMessage {
  void authorizeUsing(Authorization authorizationRules);

  void writeTo(DataDestination dataDestination);
}
