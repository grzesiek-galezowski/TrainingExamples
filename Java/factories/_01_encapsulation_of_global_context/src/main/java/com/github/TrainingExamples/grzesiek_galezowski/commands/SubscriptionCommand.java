package com.github.TrainingExamples.grzesiek_galezowski.commands;

public interface SubscriptionCommand extends Command {
    void validateData();

    void authorize();

    void resolve();
}
