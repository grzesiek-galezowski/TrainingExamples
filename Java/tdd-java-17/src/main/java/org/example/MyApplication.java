package org.example;

public class MyApplication {
    Command getTranslateCommand() {
        return new TranslateCommand(
            new HttpResponseBuilder(),
            new GoogleTranslationApi(),
            new TranslationRequestFromData(
                new TranslateRequestData("A", "b")));
    }
}
