package com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition.SwitchCriterias;

import com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition.SwitchCriteria;

import java.util.Arrays;

public class CompoundSwitchCriteria implements SwitchCriteria
  {
    private final SwitchCriteria[] _switchCriterias;

    public CompoundSwitchCriteria(SwitchCriteria...switchCriterias)
    {
      _switchCriterias = switchCriterias;
    }

    public boolean isNotMet()
    {
      return Arrays.stream(_switchCriterias).anyMatch(criteria -> criteria.isNotMet());
    }
  }
