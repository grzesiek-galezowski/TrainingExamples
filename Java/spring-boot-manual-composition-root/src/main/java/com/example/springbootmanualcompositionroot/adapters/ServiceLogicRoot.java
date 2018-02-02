package com.example.springbootmanualcompositionroot.adapters;

import com.example.springbootmanualcompositionroot.application.ApplicationLogicRoot;
import com.example.springbootmanualcompositionroot.application.BookRepository;
import org.springframework.context.ConfigurableApplicationContext;

public class ServiceLogicRoot {
    private ConfigurableApplicationContext container;

    public ServiceLogicRoot(ConfigurableApplicationContext container) {
        this.container = container;
    }

    public void start() {
        ApplicationLogicRoot applicationLogicRoot
            = new ApplicationLogicRoot(
            container.getBean(BookRepository.class));

        container.getBeanFactory().registerSingleton(
            Integer.toString(applicationLogicRoot.getCommandsFactory().hashCode()),
            applicationLogicRoot.getCommandsFactory()
        );

    }

    public void stop() {
        System.out.println("=============");
        System.out.println("=============");
        System.out.println("=============");
        System.out.println("=============");
        System.out.println("=============");
    }
}
