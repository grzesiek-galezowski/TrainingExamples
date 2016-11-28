package com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.InMessages;

import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Interfaces.AcmeMessage;
import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Interfaces.DataDestination;
import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Services.ActiveDirectoryBasedAuthorization;

public class StartMessage implements AcmeMessage {
    public void authorizeUsing(ActiveDirectoryBasedAuthorization authorizationRules) {
      System.out.println("Authorizing start with " + authorizationRules);
    }

    public void writeTo(DataDestination dataDestination) {
      dataDestination.add("Writing start to " + dataDestination);
    }
  }
