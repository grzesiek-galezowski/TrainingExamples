package com.example.helloworld;

import com.codahale.metrics.health.HealthCheck;

public class MessageTemplateHealthCheck extends HealthCheck {
  private final String template;

  public MessageTemplateHealthCheck(String template) {
    super();
    this.template = template;
  }

  @Override
  protected Result check() throws Exception {
    final String saying = String.format(template, "TEST");
    if (!saying.contains("TEST")) {
      return Result.unhealthy("template doesn't include a name");
    }
    return Result.healthy();
  }
}