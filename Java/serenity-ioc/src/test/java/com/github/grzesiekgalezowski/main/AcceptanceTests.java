package com.github.grzesiekgalezowski.main;

import cucumber.api.CucumberOptions;
import net.serenitybdd.cucumber.CucumberWithSerenity;
import org.junit.runner.RunWith;

@RunWith(CucumberWithSerenity.class)
@CucumberOptions(features="src/test/resources/features", plugin = "html:build/reports/cucumber")
public class AcceptanceTests {

}

