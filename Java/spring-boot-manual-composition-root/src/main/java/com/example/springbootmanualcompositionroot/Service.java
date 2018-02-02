package com.example.springbootmanualcompositionroot;

import com.example.springbootmanualcompositionroot.adapters.ServiceLogicRoot;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.ConfigurableApplicationContext;

@SpringBootApplication(scanBasePackages = "com.example.springbootmanualcompositionroot")
public class Service {

	public Service(ConfigurableApplicationContext ctx) {
		ServiceLogicRoot serviceLogicRoot
			= new ServiceLogicRoot(ctx);
		serviceLogicRoot.start();

		Runtime.getRuntime().addShutdownHook(
			new Thread(serviceLogicRoot::stop));

	}

	public static void main(String[] args) {
		SpringApplication.run(Service.class, args);
		System.out.println("lol");
	}

}
