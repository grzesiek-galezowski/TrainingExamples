package org.github.grzesiekgalezowski.examples;

import org.github.grzesiekgalezowski.examples.domain.BusinessEntitlement;
import org.github.grzesiekgalezowski.examples.domain.Entitlement;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;

import static java.lang.System.out;

public class Main {

  public static void main(String[] args) {
    out.println("begin");
    AnnotationConfigApplicationContext context
        = new AnnotationConfigApplicationContext();

    context.scan("org.github.grzesiekgalezowski.examples");
    context.refresh();

    Entitlement bean = context.getBean(Entitlement.class);
    if(bean == null) {
      out.println("NULL");
    } else {
      out.println("OK");
    }
  }

}
