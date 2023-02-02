package org.example;

public final class TranslationRequestFromData implements TranslationRequest {
    private final TranslateRequestData requestData;

    public TranslationRequestFromData(TranslateRequestData requestData) {
        this.requestData = requestData;
    }

    @Override
    public void translateUsing(
        TranslationApi translationApi,
        TranslateResponseBuilder responseBuilder) {
        String translatedText = translationApi.translate(
            requestData.text(),
            requestData.language());
        responseBuilder.success(translatedText);
    }

    @Override
    public boolean isForSupportedLanguage() {
        throw new RuntimeException(); //todo
    }

    @Override
    public void notifyTelemetry() {
        throw new RuntimeException("lol"); //todo
    }
}