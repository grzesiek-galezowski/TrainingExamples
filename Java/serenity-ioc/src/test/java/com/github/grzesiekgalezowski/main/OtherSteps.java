package com.github.grzesiekgalezowski.main;

import cucumber.api.java.en.When;

import static java.lang.System.out;

public class OtherSteps {
  public OtherSteps(MyDependency d) {
    out.println(d.toString());
  }

  @When("I search for items containing '(.*)'")
  public void searchByKeyword(String keyword) {
  }

}
