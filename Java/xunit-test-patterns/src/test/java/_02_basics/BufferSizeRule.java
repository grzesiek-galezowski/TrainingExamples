package _02_basics;

public class BufferSizeRule {
    public boolean canHandle(String s) {
        return s != null && s.length() < 4;
    }
}
