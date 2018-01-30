package com.example.springbootmanualcompositionroot.bootstrap;

import com.example.springbootmanualcompositionroot.adapters.PlainCommandsFactory;
import com.example.springbootmanualcompositionroot.adapters.SimpleBookRepository;
import com.example.springbootmanualcompositionroot.application.ApplicationLogicRoot;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.ApplicationContext;
import org.springframework.web.context.support.GenericWebApplicationContext;

@SpringBootApplication
public class Service {


	public Service(ApplicationContext applicationContext) {
		System.out.println("==========lololo");

		final ApplicationLogicRoot applicationLogicRoot
			= new ApplicationLogicRoot(new SimpleBookRepository());
		applicationLogicRoot.compose();

		PlainCommandsFactory commandsFactory = applicationLogicRoot.getCommandsFactory();

		((GenericWebApplicationContext) applicationContext).getBeanFactory().registerSingleton(
			commandsFactory.getClass().toString(),
			commandsFactory);

	}

	public static void main(String[] args) {
		SpringApplication.run(Service.class, args);
		System.out.println("lol");
	}

}
