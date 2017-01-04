package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Services.Authorization;

public interface InboundMessage {
  void authorizeUsing(Authorization authorizationRules);

  void writeTo(DataDestination dataDestination);
}
