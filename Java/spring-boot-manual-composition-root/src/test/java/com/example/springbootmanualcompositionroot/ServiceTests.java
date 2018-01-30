package com.example.springbootmanualcompositionroot;

import com.example.springbootmanualcompositionroot.adapters.BooksController;
import com.example.springbootmanualcompositionroot.application.Book;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import static org.assertj.core.api.Assertions.assertThat;

@RunWith(SpringRunner.class)
@SpringBootTest
public class ServiceTests {

  @Autowired
  private BooksController controller;

  @Test
	public void contextLoads() {
    assertThat(controller).isNotNull();
    Book bookByIsbn = controller.getBookByIsbn("1234");
    assertThat(bookByIsbn.getIsbn()).isEqualTo("1234");
  }

}
