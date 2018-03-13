package other;

import authorization.AuthorizationStructure;
import com.github.TrainingExamples.grzesiek_galezowski.commands.AssetQueriesFactory;
import com.github.TrainingExamples.grzesiek_galezowski.commands.CommandFactory;
import dto.NewSubscriptionParametersDto;
import dto.StartSubscriptionResponseDto;
import dto.StopSubscriptionResponseDto;
import dto.StoppedSubscriptionParametersDto;
import subscriptions.SubscriptionDataCorrectnessCriteria;
import subscriptions.SubscriptionFactory;
import subscriptions.Subscriptions;

public class Program {

    public static void main(String[] args) {
        AuthorizationStructure structure = new AuthorizationStructure();

        Log dummyLog = new DummyLog();
        Api api = new Api(
            new CommandFactory(
                new Subscriptions(),
                structure,
                new SubscriptionFactory(),
                new SubscriptionDataCorrectnessCriteria(),
                new AssetQueriesFactory(
                    structure
                ),
                dummyLog
            ),
            new DefaultResponseBuilderFactory(),
            dummyLog,
            structure,
            new SubscriptionFactory(),
            new Subscriptions(), structure);

        StartSubscriptionResponseDto response1 = api.startSubscription(new NewSubscriptionParametersDto());
        StopSubscriptionResponseDto response2 = api.stopSubscription(new StoppedSubscriptionParametersDto());
    }
}
