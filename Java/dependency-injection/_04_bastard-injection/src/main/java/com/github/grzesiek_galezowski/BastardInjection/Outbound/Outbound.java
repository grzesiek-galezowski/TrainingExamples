package com.github.grzesiek_galezowski.BastardInjection.Outbound;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.Message;

/**
 * Created by grzes on 28.11.2016.
 */
public interface Outbound {
  void send(Message message);
}
