package com.github.TrainingExamples.grzesiek_galezowski;

public interface Message {
  void validate();

  void authorize();

  void respond();

  void handle();
}
