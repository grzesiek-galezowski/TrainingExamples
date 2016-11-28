package com.github.grzesiek_galezowski.BastardInjection.Outbound;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.AcmeMessage;

/**
 * Created by grzes on 28.11.2016.
 */
public interface IOutbound {
  void send(AcmeMessage message);
}
