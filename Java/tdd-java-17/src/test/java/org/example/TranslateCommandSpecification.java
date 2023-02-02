package org.example;

import autofixture.publicinterface.Any;
import org.junit.jupiter.api.Test;
import org.mockito.InOrder;
import org.mockito.Mockito;

import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.when;

class TranslateCommandSpecification {

    @Test
    public void shouldTODO() { //todo
        //GIVEN
        var responseBuilder = Any.instanceOf(TranslateResponseBuilder.class);
        var translationApi = Any.instanceOf(TranslationApi.class);
        var request = mock(TranslationRequest.class);
        var command = new TranslateCommand(
            responseBuilder,
            translationApi,
            request);

        when(request.isForSupportedLanguage()).thenReturn(true);

        //WHEN
        command.execute();

        //THEN
        InOrder inOrder = Mockito.inOrder(request);
        inOrder.verify(request).translateUsing(translationApi, responseBuilder);
        inOrder.verify(request).notifyTelemetry();
    }

    @Test
    public void shouldReportErrorWhenRequestIsNotForSupportedLanguage() {
        //GIVEN
        var responseBuilder = mock(TranslateResponseBuilder.class);
        var request = mock(TranslationRequest.class);
        var command = new TranslateCommand(
            responseBuilder,
            Any.instanceOf(TranslationApi.class),
            request);

        when(request.isForSupportedLanguage()).thenReturn(false);

        //WHEN
        command.execute();

        //THEN
        verify(responseBuilder).failedBecauseLanguageIsNotSupported();
    }
}