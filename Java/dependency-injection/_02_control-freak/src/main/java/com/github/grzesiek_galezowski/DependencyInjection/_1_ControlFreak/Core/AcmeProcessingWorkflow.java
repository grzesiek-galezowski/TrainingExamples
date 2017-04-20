package com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Core;

import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Interfaces.AcmeMessage;
import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Outbound.XmlTcpOutbound;
import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Services.ActiveDirectoryBasedAuthorization;
import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Services.MsSqlBasedRepository;

public class AcmeProcessingWorkflow {
    private final MsSqlBasedRepository repository;
    private final ActiveDirectoryBasedAuthorization authorizationRules;
    private XmlTcpOutbound _outbound;

    public AcmeProcessingWorkflow() {
      authorizationRules = new ActiveDirectoryBasedAuthorization();
      repository = new MsSqlBasedRepository();
    }

    public void setOutbound(XmlTcpOutbound outbound) {
      _outbound = outbound;
    }

    public void applyTo(AcmeMessage message) {
      message.authorizeUsing(authorizationRules);
      repository.save(message);
      _outbound.send(message);
    }
  }
