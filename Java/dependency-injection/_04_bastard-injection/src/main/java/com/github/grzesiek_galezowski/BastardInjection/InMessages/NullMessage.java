package com.github.grzesiek_galezowski.BastardInjection.InMessages;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.Message;
import com.github.grzesiek_galezowski.BastardInjection.Interfaces.DataDestination;
import com.github.grzesiek_galezowski.BastardInjection.Services.IAuthorization;

public class NullMessage implements Message {
  public void authorizeUsing(IAuthorization authorizationRules) {

  }

  public void writeTo(DataDestination dataDestination) {

  }
}