package com.example.springbootmanualcompositionroot.adapters;

import com.example.springbootmanualcompositionroot.adapters.SimpleBookRepository;
import com.example.springbootmanualcompositionroot.application.BookRepository;
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
