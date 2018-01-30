package com.example.springbootmanualcompositionroot.application;

public interface CommandsFactory {
  BookQuery createQueryByIsbn(final String isbn);
}
