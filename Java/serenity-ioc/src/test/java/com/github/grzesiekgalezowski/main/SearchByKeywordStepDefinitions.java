package com.github.grzesiekgalezowski.main;

import cucumber.api.java.en.Given;
import cucumber.api.java.en.Then;
import cucumber.api.java.en.When;

import static java.lang.System.out;

public class SearchByKeywordStepDefinitions {


  public SearchByKeywordStepDefinitions(MyDependency d) {
    out.println(d.toString());
  }

  @Given("I want to buy (.*)")
  public void buyerWantsToBuy(String article) {
  }

  @Then("I should only see items related to '(.*)'")
  public void resultsForACategoryAndKeywordInARegion(String keyword) {
  }
}
