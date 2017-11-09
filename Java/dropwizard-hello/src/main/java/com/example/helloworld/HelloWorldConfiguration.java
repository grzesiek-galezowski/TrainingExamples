package com.example.helloworld;

import com.fasterxml.jackson.annotation.JsonProperty;
import io.dropwizard.Configuration;
import org.hibernate.validator.constraints.NotEmpty;

public class HelloWorldConfiguration extends Configuration {
  @NotEmpty
  @JsonProperty
  private String template;

  @NotEmpty
  @JsonProperty
  private String defaultName = "Stranger";

  public String getTemplate() { //json template
    return template;
  }

  public String getDefaultName() {
    return defaultName;
  }
}