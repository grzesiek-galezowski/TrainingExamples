package other;

public class DummyLog implements Log {
    @Override
    public void error(Exception exception) {
        System.out.println(exception);
    }
}
