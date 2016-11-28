package com.github.grzesiek_galezowski.test_environment;

import org.assertj.core.api.Condition;
import org.hamcrest.Matcher;
import org.mutabilitydetector.AnalysisResult;
import org.mutabilitydetector.MutableReasonDetail;

import java.util.Arrays;

import static org.mutabilitydetector.unittesting.MutabilityAssert.assertInstancesOf;

/**
 * Created by astral on 26.03.2016.
 */
public class ImmutableCondition<T> extends Condition<Class<T>> {

  private static final int TWO_MATCHER_ARGS = 2;
  private static final int THREE_MATCHER_ARGS = 3;
  private static final int FOUR_MATCHER_ARGS = 4;
  private static final int INDEX_OF_FIRST_NON_EXPLICIT_MATCHER_ON_ARG_LIST = 3;
  private final Matcher<MutableReasonDetail>[] matchers;
  private final Matcher<AnalysisResult> mutabilityMatcher;

  public ImmutableCondition(final Matcher<MutableReasonDetail>[] matchers, final Matcher<AnalysisResult> mutabilityMatcher) {
    this.matchers = matchers.clone();
    this.mutabilityMatcher = mutabilityMatcher;
  }

  @Override
  public boolean matches(final Class<T> t) {
    try {
      if (matchers.length == 0) {
        assertInstancesOf(t, mutabilityMatcher);
      } else if (matchers.length == 1) {
        assertInstancesOf(t, mutabilityMatcher, matchers[0]);
      } else if (matchers.length == TWO_MATCHER_ARGS) {
        assertInstancesOf(t, mutabilityMatcher, matchers[0], matchers[1]);
      } else if (matchers.length == THREE_MATCHER_ARGS) {
        assertInstancesOf(t, mutabilityMatcher, matchers[0], matchers[1], matchers[2]);
      } else if (matchers.length == FOUR_MATCHER_ARGS) {
        assertInstancesOf(t, mutabilityMatcher,
            matchers[0],
            matchers[1],
            matchers[2],
            Arrays.copyOfRange(matchers,
                INDEX_OF_FIRST_NON_EXPLICIT_MATCHER_ON_ARG_LIST, matchers.length));
      }

    } catch (final Throwable throwable) {
      describedAs(throwable.getMessage());
      return false;
    }
    return true;
  }
}
