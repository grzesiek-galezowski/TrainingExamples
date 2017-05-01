package com.github.grzesiekgalezowski.main;

import cucumber.api.Scenario;
import cucumber.api.java.Before;
import cucumber.api.java.en.Given;
import cucumber.api.java.en.Then;
import cucumber.api.java.en.When;
import org.junit.Assert;

import static java.lang.System.out;

public class SearchByKeywordStepDefinitions {

  Scenario scenario;
  private MyDependency d;

  @Before
  public void before(Scenario scenario) {
    this.scenario = scenario;
  }


  public SearchByKeywordStepDefinitions(MyDependency d) {
    this.d = d;
  }

  @Given("I want to buy (.*)")
  public void buyerWantsToBuy(String article) {
    scenario.write("Logged: " + article);
  }

  @Then("I should only see items related to '(.*)'")
  public void resultsForACategoryAndKeywordInARegion(String keyword) {
    scenario.write("Logged: " + keyword);
    Assert.assertFalse(true);
  }
}
