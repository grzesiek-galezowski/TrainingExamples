package com.github.grzesiek_galezowski.BastardInjection.Inbound;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.AcmeMessage;

/**
 * Created by grzes on 28.11.2016.
 */
interface IPacketParsing {
  AcmeMessage resultFor(byte[] frameData);
}
