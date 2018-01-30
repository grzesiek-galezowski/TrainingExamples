package com.example.springbootmanualcompositionroot.adapters;

import com.example.springbootmanualcompositionroot.application.Book;
import com.example.springbootmanualcompositionroot.application.CommandsFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;

@Controller
public class BooksController {

  private CommandsFactory commandsFactory;

  @Autowired
  public BooksController(CommandsFactory commandsFactory) {
    this.commandsFactory = commandsFactory;
  }

  @RequestMapping("/books/by_isbn")
  public Book getBookByIsbn(
      @RequestParam String isbn) {
    return commandsFactory.createQueryByIsbn(isbn).execute();
  }

}