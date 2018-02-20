public interface Message {
  void validate();

  void authorize();

  void respond();

  void handle();
}
