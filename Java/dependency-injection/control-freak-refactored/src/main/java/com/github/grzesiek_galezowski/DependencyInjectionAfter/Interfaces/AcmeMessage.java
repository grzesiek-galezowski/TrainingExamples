package com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Services.IAuthorization;

public interface AcmeMessage {
  void AuthorizeUsing(IAuthorization authorizationRules);

  void WriteTo(DataDestination dataDestination);
}
