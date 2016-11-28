package com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Core;

import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Interfaces.AcmeMessage;
import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Outbound.XmlTcpOutbound;
import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Services.ActiveDirectoryBasedAuthorization;
import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Services.MsSqlBasedRepository;

public class AcmeProcessingWorkflow {
    private final MsSqlBasedRepository _repository;
    private final ActiveDirectoryBasedAuthorization _authorizationRules;
    private XmlTcpOutbound _outbound;

    public AcmeProcessingWorkflow() {
      _authorizationRules = new ActiveDirectoryBasedAuthorization();
      _repository = new MsSqlBasedRepository();
    }

    public void setOutbound(XmlTcpOutbound outbound) {
      _outbound = outbound;
    }

    public void applyTo(AcmeMessage message) {
      message.authorizeUsing(_authorizationRules);
      _repository.save(message);
      _outbound.send(message);
    }
  }
