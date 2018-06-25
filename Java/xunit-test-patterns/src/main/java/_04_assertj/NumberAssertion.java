package _04_assertj;

public class NumberAssertion {
    public void applyTo(int i) {
        if(i < 0) {
            throw new RuntimeException("Trolololo");
        }
    }
}
