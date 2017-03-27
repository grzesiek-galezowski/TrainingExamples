package com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Services.Authorization;

public interface AcmeMessage {
  void AuthorizeUsing(Authorization authorizationRules);

  void WriteTo(DataDestination dataDestination);
}
