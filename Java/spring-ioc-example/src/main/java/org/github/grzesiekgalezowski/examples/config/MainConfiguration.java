package org.github.grzesiekgalezowski.examples.config;

import org.github.grzesiekgalezowski.examples.domain.Destination;
import org.github.grzesiekgalezowski.examples.domain.Entitlement;
import org.github.grzesiekgalezowski.examples.domain.Output;
import org.github.grzesiekgalezowski.examples.domain.Source;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.annotation.Configuration;

@Configuration
@ComponentScan(basePackages = "org.github.grzesiekgalezowski.examples")
public class MainConfiguration  {

  @Bean(name="entitlement1")
  public Entitlement entitlement(
      Output output,
      @Qualifier("entitlementName") String str){
    Entitlement ent= new Entitlement(output, str);
    return ent;
  }

  @Bean(name="entitlement2")
  public Entitlement decoratedEntitlement(
      @Qualifier("entitlement1") Entitlement e,
      @Qualifier("decoratorName") String str) {
    return new EntitlementDecorator(e, str);
  }

  @Bean(name = "decoratorName")
  public String decoratorName() {
    return "lolek";
  }

  @Bean(name = "entitlementName")
  public String entitlementName() {
    return "lolek112";
  }

  public Entitlement getMyEntitlement() {
        return new EntitlementDecorator(
            new Entitlement(
                new Destination(
                    new Source()), "lolek112"),
            "lolek");
  }
}