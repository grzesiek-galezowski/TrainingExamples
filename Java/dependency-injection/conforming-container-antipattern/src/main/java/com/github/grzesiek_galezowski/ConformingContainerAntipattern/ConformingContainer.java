package com.github.grzesiek_galezowski.ConformingContainerAntipattern;

import org.picocontainer.ComponentMonitor;
import org.picocontainer.DefaultPicoContainer;
import org.picocontainer.MutablePicoContainer;
import org.picocontainer.lifecycle.ReflectionLifecycleStrategy;
import org.picocontainer.monitors.NullComponentMonitor;

public class ConformingContainer implements AutoCloseable {
  final MutablePicoContainer _container = createContainer();

  public MutablePicoContainer createContainer() {
    ComponentMonitor monitor = new NullComponentMonitor();
    return new DefaultPicoContainer(
        monitor,
        new ReflectionLifecycleStrategy(monitor),
        null);
  }

  public <T> As<T> as(Class<T> clazz) {
    return new As(_container, clazz);
  }

  public <T> void useSingle(Class<T> clazz) {
    as(clazz).useSingle(clazz);
  }

  public <T> void useCreated(Class<T> clazz) {
    as(clazz).useCreated(clazz);
  }

  public <T> T resolve(Class<T> clazz) {
    return _container.getComponent(clazz);
  }

  public void close() throws Exception {
    _container.dispose();
  }

}

