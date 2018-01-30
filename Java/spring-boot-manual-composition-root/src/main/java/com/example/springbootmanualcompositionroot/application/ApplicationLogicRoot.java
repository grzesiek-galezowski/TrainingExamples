package com.example.springbootmanualcompositionroot.application;

import com.example.springbootmanualcompositionroot.adapters.PlainCommandsFactory;
import com.example.springbootmanualcompositionroot.application.BookRepository;

public class ApplicationLogicRoot {
  private BookRepository bookRepository;
  private PlainCommandsFactory commandsFactory;

  public ApplicationLogicRoot(final BookRepository bookRepository) {

    this.bookRepository = bookRepository;
  }

  public void compose() {
    this.commandsFactory = new PlainCommandsFactory(bookRepository);
  }

  public PlainCommandsFactory getCommandsFactory() {
    return commandsFactory;
  }
}
