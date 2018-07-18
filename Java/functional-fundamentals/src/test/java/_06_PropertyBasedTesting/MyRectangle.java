package _06_PropertyBasedTesting;

public class MyRectangle {
    private final Integer width;
    private final Integer height;

    public MyRectangle(Integer width, Integer height) {
        this.width = width;
        this.height = height;
    }

    public Integer getWidth() {
        return width;
    }

    public Integer getHeight() {
        return height;
    }

    public Integer getField() {
        return width * height;
    }
}
