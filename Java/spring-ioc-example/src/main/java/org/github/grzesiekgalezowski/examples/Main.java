package org.github.grzesiekgalezowski.examples;

import org.github.grzesiekgalezowski.examples.domain.Entitlement;
import org.springframework.context.ApplicationContext;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;
import org.springframework.context.support.StaticApplicationContext;

public class Main {

  public static void main(String[] args) {
    AnnotationConfigApplicationContext context
        = new AnnotationConfigApplicationContext();

    context.scan("org.github.grzesiekgalezowski.examples");
    context.refresh();

    Entitlement bean = context.getBean(
        "entitlement2", Entitlement.class);
    if(bean == null) {
      System.out.println("NULL");
    } else {
      System.out.println("OK");
    }
  }

  public static void main2(String[] args) {
    StaticApplicationContext context
        = new StaticApplicationContext();

    context.registerBeanDefinition("a", new );
    context.refresh();

    Entitlement bean = context.getBean(
        "entitlement2", Entitlement.class);
    if(bean == null) {
      System.out.println("NULL");
    } else {
      System.out.println("OK");
    }
  }

}
