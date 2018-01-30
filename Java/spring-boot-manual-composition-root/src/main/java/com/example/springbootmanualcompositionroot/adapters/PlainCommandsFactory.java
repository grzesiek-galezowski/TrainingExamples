package com.example.springbootmanualcompositionroot.adapters;

import com.example.springbootmanualcompositionroot.application.BookQuery;
import com.example.springbootmanualcompositionroot.application.BookRepository;
import com.example.springbootmanualcompositionroot.application.CommandsFactory;
import com.example.springbootmanualcompositionroot.application.PlainBookQuery;

public class PlainCommandsFactory implements CommandsFactory {
  private BookRepository repository;

  public PlainCommandsFactory(final BookRepository repository) {
    this.repository = repository;
  }

  @Override
  public BookQuery createQueryByIsbn(final String isbn) {
    return new PlainBookQuery(this.repository, isbn);
  }
}
