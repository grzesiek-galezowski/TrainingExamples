package com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Interfaces;

import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Services.ActiveDirectoryBasedAuthorization;

public interface AcmeMessage {
  void authorizeUsing(ActiveDirectoryBasedAuthorization authorizationRules);

  void writeTo(DataDestination dataDestination);
}
