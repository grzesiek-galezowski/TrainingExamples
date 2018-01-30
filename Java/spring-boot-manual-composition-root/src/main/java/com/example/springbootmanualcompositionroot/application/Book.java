package com.example.springbootmanualcompositionroot.application;

import lombok.Getter;

public class Book {

  private final String isbn;

  private final String name;

  public Book(final String isbn, final String name) {

    this.isbn = isbn;
    this.name = name;
  }

  public String getIsbn() {
    return isbn;
  }

  public String getName() {
    return name;
  }
}
