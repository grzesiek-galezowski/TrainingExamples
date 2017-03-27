package com.github.grzesiek_galezowski.BastardInjection.InMessages;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.Message;
import com.github.grzesiek_galezowski.BastardInjection.Interfaces.DataDestination;
import com.github.grzesiek_galezowski.BastardInjection.Services.Authorization;

public class NullMessage implements Message {
  public void authorizeUsing(Authorization authorizationRules) {

  }

  public void writeTo(DataDestination dataDestination) {

  }
}