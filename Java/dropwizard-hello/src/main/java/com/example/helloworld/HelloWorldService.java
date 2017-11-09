package com.example.helloworld;

import io.dropwizard.Application;
import io.dropwizard.setup.Bootstrap;
import io.dropwizard.setup.Environment;

public class HelloWorldService extends Application<HelloWorldConfiguration> {
  public static void main(String[] args) throws Exception {
    //!!! run with arguments "server src/main/resources/hello-world.yml"

    new HelloWorldService().run(args);
  }

  @Override
  public void initialize(Bootstrap<HelloWorldConfiguration> bootstrap) {

  }

  @Override
  public void run(HelloWorldConfiguration configuration,
                  Environment environment) {
    registerRestControllers(configuration, environment);
    registerStartupChecks(configuration, environment);
  }

  private void registerStartupChecks(final HelloWorldConfiguration configuration, final Environment environment) {
    environment.healthChecks().register("templateCheck",
        new MessageTemplateHealthCheck(configuration.getTemplate()));
  }

  private void registerRestControllers(final HelloWorldConfiguration configuration, final Environment environment) {
    environment.jersey().register(
        new HelloWorldResource(
            configuration.getTemplate(),
            configuration.getDefaultName()));
  }

}