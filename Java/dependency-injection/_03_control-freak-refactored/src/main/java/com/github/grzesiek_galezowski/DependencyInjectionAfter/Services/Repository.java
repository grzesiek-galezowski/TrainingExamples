package com.github.grzesiek_galezowski.DependencyInjectionAfter.Services;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.AcmeMessage;

public interface Repository {
  void Save(AcmeMessage message);
}
