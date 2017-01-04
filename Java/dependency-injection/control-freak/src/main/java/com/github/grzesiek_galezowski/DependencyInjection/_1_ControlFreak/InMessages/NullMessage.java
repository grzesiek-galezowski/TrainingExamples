package com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.InMessages;

import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Interfaces.AcmeMessage;
import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Interfaces.DataDestination;
import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Services.ActiveDirectoryBasedAuthorization;

public class NullMessage implements AcmeMessage {
    public void authorizeUsing(ActiveDirectoryBasedAuthorization authorizationRules) {

    }

    public void writeTo(DataDestination dataDestination) {

    }
  }
