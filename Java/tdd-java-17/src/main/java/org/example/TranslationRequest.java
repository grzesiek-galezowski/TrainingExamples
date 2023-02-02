package org.example;

public interface TranslationRequest {
    void translateUsing(
        TranslationApi translationApi,
        TranslateResponseBuilder responseBuilder);

    boolean isForSupportedLanguage();

    void notifyTelemetry();
}
