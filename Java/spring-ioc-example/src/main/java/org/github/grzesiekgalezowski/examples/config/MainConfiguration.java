package org.github.grzesiekgalezowski.examples.config;

import org.github.grzesiekgalezowski.examples.domain.*;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.beans.factory.config.ConfigurableBeanFactory;
import org.springframework.context.annotation.*;

//todo remove one factory method and see error message
//todo mention primary
//todo mention scopes

@Configuration
@ComponentScan(basePackages = "org.github.grzesiekgalezowski.examples")
public class MainConfiguration  {

  private final String destinationCache = "destinationCache";
  private final String entitlementCache = "entitlementCache";
  private final String entitlementName = "entitlementName";
  private final String decoratorName = "decoratorName";
  private final String entitlementDestination = "entitlementDestination";

  @Bean
  @Primary
  public Output getOutput(
      Cache cache,
      Source source) {
    return new Destination(cache, source);
  }

  @Bean(name="entitlement1")
  public Entitlement entitlement(
      final Output output,
      @Qualifier(entitlementName) final String str,
      @Qualifier(entitlementCache) final Cache cache){
    BusinessEntitlement ent = new BusinessEntitlement(cache, output, str);
    return ent;
  }

  @Primary
  public Entitlement decoratedEntitlement(
      @Qualifier("entitlement1") Entitlement e,
      @Qualifier(decoratorName) String str) {
    return new EntitlementDecorator(e, str);
  }

  @Bean(name = decoratorName)
  public String decoratorName() {
    return "lolek";
  }

  @Bean(name = entitlementName)
  public String entitlementName() {
    return "lolek112";
  }

  @Bean(name = entitlementCache)
  public Cache getEntitlementCache() {
        return new InMemoryCache();
  }

  @Bean(name = destinationCache)
  @Primary
  @Scope(scopeName = ConfigurableBeanFactory.SCOPE_PROTOTYPE)
  public Cache getCache() {
    return new PersistentCache();
  }

  @Bean
  public Source getSource(
      final Cache cache) {
    return new Source(cache);
  }

}