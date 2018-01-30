package com.example.springbootmanualcompositionroot.application;

import lombok.Getter;

public class Book {
  @Getter
  private final String isbn;
  @Getter
  private final String name;

  public Book(final String isbn, final String name) {

    this.isbn = isbn;
    this.name = name;
  }
}
