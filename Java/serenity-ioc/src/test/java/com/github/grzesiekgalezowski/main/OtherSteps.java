package com.github.grzesiekgalezowski.main;

import cucumber.api.Scenario;
import cucumber.api.java.en.When;
import org.junit.Assert;
import cucumber.api.java.Before;

import static java.lang.System.out;

public class OtherSteps {

  Scenario scenario;

  @Before
  public void before(Scenario scenario) {
    this.scenario = scenario;
  }

  public OtherSteps(MyDependency d) {
    out.println(d.toString());
  }

  @When("I search for items containing '(.*)'")
  public void searchByKeyword(String keyword) {
    scenario.write("Logged: " + keyword);
  }

}
