package com.example.springbootmanualcompositionroot.adapters;

import com.example.springbootmanualcompositionroot.application.Book;
import com.example.springbootmanualcompositionroot.application.BookRepository;
import org.springframework.cache.annotation.Cacheable;

public class SimpleBookRepository implements BookRepository {

  @Override
  @Cacheable("books")
  public Book getByIsbn(String isbn) {
    return new Book(isbn, "Some book");
  }

}