package logic;

import lombok.EqualsAndHashCode;

import java.nio.file.Path;
import java.nio.file.Paths;

@EqualsAndHashCode
public class FileName {

    private Path path;

    public FileName(Path path) {
        this.path = path;
    }

    public static FileName from(String name) {
        return new FileName(Paths.get(name));
    }

    @Override
    public String toString() {
        return path.toString();
    }
}
