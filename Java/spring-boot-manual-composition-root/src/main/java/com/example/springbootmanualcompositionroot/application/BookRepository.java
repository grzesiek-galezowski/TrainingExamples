package com.example.springbootmanualcompositionroot.application;

public interface BookRepository {
  Book getByIsbn(String isbn);
}
