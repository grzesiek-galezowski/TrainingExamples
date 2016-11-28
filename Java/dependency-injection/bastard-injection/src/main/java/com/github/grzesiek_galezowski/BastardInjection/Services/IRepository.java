package com.github.grzesiek_galezowski.BastardInjection.Services;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.AcmeMessage;

/**
 * Created by grzes on 28.11.2016.
 */
public interface IRepository {
  void save(AcmeMessage message);
}
