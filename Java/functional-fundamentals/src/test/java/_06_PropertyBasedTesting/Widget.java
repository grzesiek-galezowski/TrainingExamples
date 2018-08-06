package _06_PropertyBasedTesting;

public class Widget {
    private final Integer width;
    private final Integer height;

    public Widget(Integer width, Integer height) {
        this.width = width;
        this.height = height;
    }

    public boolean isValid() {
        return width > 0 && height > 0;
    }

    @Override
    public String toString() {
        return "Widget{" +
            "width=" + width +
            ", height=" + height +
            '}';
    }
}
