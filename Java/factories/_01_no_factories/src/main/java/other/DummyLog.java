package other;

public class DummyLog implements Log {
    public void Error(Exception exception) {
        System.out.println(exception);
    }
}
