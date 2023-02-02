package org.example;

public interface TranslateResponseBuilder {
    void success(String translatedText);

    void failedBecauseLanguageIsNotSupported();
}
