package com.example.springbootmanualcompositionroot.application;

public class PlainBookQuery implements BookQuery {

  private BookRepository bookRepository;
  private String isbn;

  public PlainBookQuery(BookRepository bookRepository, final String isbn) {
    this.bookRepository = bookRepository;
    this.isbn = isbn;
  }

  @Override
  public Book execute() {
    return bookRepository.getByIsbn(isbn);
  }
}
