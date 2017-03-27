package com.github.grzesiek_galezowski.ConformingContainerAntipattern;

import org.picocontainer.MutablePicoContainer;

import static org.picocontainer.Characteristics.CACHE;
import static org.picocontainer.Characteristics.NO_CACHE;

public class As<T> {
  private final MutablePicoContainer _container;
  private final Class<T> baseClass;

  public As(MutablePicoContainer container, Class<T> baseClass) {
    _container = container;
    this.baseClass = baseClass;
  }

  public <U extends T> void useSingle(Class<U> clazz) {
    _container.as(CACHE).addComponent(baseClass, clazz);
  }

  public <U extends T> void useCreated(Class<U> clazz) {
    _container.as(NO_CACHE).addComponent(baseClass, clazz);
  }

  public void Use(T instance) {
    _container.addComponent(instance);
  }
}
