package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Services;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces.InboundMessage;

public interface Repository {
  void save(InboundMessage message);
}
