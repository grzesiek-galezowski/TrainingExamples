package com.randori.inbound.logic;

public interface Errors {
    Boolean hasAny();

    void print();

    void addError(String errorString);
}
