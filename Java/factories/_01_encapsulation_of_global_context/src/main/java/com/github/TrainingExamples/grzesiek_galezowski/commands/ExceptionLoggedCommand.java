package com.github.TrainingExamples.grzesiek_galezowski.commands;

import other.Log;

public class ExceptionLoggedCommand implements Command {
    private final Log log;
    private final Command wrappedCommand;

    public ExceptionLoggedCommand(Log log, Command wrappedCommand) {
        this.log = log;
        this.wrappedCommand = wrappedCommand;
    }

    public void invoke() {
        try {
            wrappedCommand.invoke();
        } catch (Exception e) {
            log.error(e);
        }
    }
}
