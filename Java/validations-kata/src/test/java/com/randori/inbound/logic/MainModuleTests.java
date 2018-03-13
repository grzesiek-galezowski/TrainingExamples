package com.randori.inbound.logic;

import autofixture.publicinterface.Any;
import com.randori.inbound.thirdparty.SubscriptionRequestData;
import org.mockito.InOrder;
import org.testng.annotations.Test;

import static org.mockito.BDDMockito.given;
import static org.mockito.Mockito.inOrder;
import static org.mockito.Mockito.mock;

//X x = Any.anonymous(X.class);
//X mock = mock(X.class);

//order verification:
//InOrder inOrder = inOrder(mock1, mock2, mock3);
//inOrder.verify(mock2).doSomething();
//inOrder.verify(mock1).doSomethingElse();

public class MainModuleTests {
    @Test
    //- Should use command factory to wrap
    // subscription data with
    // a command object and then
    // validate and execute the command.
    //
    public void handleTest() {
        //GIVEN
        SubscriptionRequestData input =
            Any.anonymous(SubscriptionRequestData.class);
        SubscriptionCommand command = mock(SubscriptionCommand.class);
        CommandFactory commandFactory = mock(CommandFactory.class);
        MainModule mainModule = new MainModule(commandFactory);
        given(commandFactory.createFrom(input)).willReturn(command);

        //WHEN
        mainModule.handle(input);

        //THEN
        InOrder inOrder = inOrder(command);
        inOrder.verify(command).validate();
        inOrder.verify(command).execute();
        
    }
}
