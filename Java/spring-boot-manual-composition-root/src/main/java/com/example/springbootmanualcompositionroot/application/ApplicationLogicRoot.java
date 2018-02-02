package com.example.springbootmanualcompositionroot.application;

import com.example.springbootmanualcompositionroot.adapters.PlainCommandsFactory;

public class ApplicationLogicRoot {
  private PlainCommandsFactory commandsFactory;

  public ApplicationLogicRoot(final BookRepository bookRepository) {
    this.commandsFactory =
      new PlainCommandsFactory(bookRepository);
  }

  public PlainCommandsFactory getCommandsFactory() {
    return commandsFactory;
  }
}
