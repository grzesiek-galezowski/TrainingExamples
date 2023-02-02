package org.example;

public class TranslateCommand implements Command {
    private final TranslationRequest translationRequest;
    private TranslateResponseBuilder responseBuilder;
    private final TranslationApi translationApi;

    public TranslateCommand(
        TranslateResponseBuilder responseBuilder,
        TranslationApi translationApi,
        TranslationRequest translationRequest) {
        this.responseBuilder = responseBuilder;
        this.translationApi = translationApi;
        this.translationRequest = translationRequest;
    }

    @Override
    public void execute() {
        if(translationRequest.isForSupportedLanguage()) {
            translationRequest.translateUsing(translationApi, responseBuilder);
            translationRequest.notifyTelemetry();
        } else {
            responseBuilder.failedBecauseLanguageIsNotSupported();
        }
    }

}
