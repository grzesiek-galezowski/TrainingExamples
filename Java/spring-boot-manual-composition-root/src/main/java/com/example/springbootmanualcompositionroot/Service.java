package com.example.springbootmanualcompositionroot;

import com.example.springbootmanualcompositionroot.adapters.PlainCommandsFactory;
import com.example.springbootmanualcompositionroot.application.BookRepository;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.ApplicationContext;
import org.springframework.web.context.support.GenericWebApplicationContext;

@SpringBootApplication
public class Service {


	public Service(ApplicationContext applicationContext) {

		//
		// From spring boot to app logic
		//
		PlainCommandsFactory commandsFactory
			= new PlainCommandsFactory(
				  applicationContext.getBean(BookRepository.class));

		//
		// From app logic to spring boot
		//
		((GenericWebApplicationContext) applicationContext)
			.getBeanFactory().registerSingleton(
				commandsFactory.getClass().toString(),
				commandsFactory);

	}

	public static void main(String[] args) {
		SpringApplication.run(Service.class, args);
		System.out.println("lol");
	}

}
