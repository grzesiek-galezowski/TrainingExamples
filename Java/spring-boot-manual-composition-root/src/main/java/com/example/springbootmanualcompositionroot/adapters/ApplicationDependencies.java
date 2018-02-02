package com.example.springbootmanualcompositionroot.adapters;

import com.example.springbootmanualcompositionroot.application.ApplicationLogicRoot;
import com.example.springbootmanualcompositionroot.application.BookRepository;
import com.example.springbootmanualcompositionroot.application.CommandsFactory;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.annotation.Configuration;

@Configuration
@ComponentScan(basePackages = "com.example.springbootmanualcompositionroot.*")
public class ApplicationDependencies {

  @Bean
  public BookRepository getRepo() {
    return new SimpleBookRepository();
  }


}
