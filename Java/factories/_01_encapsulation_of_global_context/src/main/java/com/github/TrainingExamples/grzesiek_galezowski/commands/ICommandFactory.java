package com.github.TrainingExamples.grzesiek_galezowski.commands;

import dto.NewSubscriptionParametersDto;
import dto.StoppedSubscriptionParametersDto;
import responseBuilders.SubscriptionStartResponseBuilder;
import responseBuilders.SubscriptionStopResponseBuilder;

public interface ICommandFactory {
    Command createFrom(NewSubscriptionParametersDto parameters, SubscriptionStartResponseBuilder responseBuilder);

    Command createFrom(StoppedSubscriptionParametersDto parameters, SubscriptionStopResponseBuilder responseBuilder);
}
