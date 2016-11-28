package com.github.grzesiek_galezowski.DependencyInjectionAfter.Services;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.DataDestination;
import sun.reflect.generics.reflectiveObjects.NotImplementedException;

public class SqlDataDestination implements DataDestination {
    public void Add(String s) {
        throw new NotImplementedException();
    }
}
