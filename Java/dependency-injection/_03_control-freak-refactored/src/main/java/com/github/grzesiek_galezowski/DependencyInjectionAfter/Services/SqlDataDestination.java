package com.github.grzesiek_galezowski.DependencyInjectionAfter.Services;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.DataDestination;

public class SqlDataDestination implements DataDestination {
    public void add(String s) {
        throw new RuntimeException("not implemented");
    }
}
