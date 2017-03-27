package com.github.grzesiek_galezowski.BastardInjection.Inbound;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.Message;

/**
 * Created by grzes on 28.11.2016.
 */
interface PacketParsing {
  Message resultFor(byte[] frameData);
}
