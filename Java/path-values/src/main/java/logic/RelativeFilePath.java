package logic;

import java.nio.file.Path;
import java.nio.file.Paths;

public final class RelativeFilePath {
    private final Path path;

    RelativeFilePath(final Path path) {

        this.path = path;
    }

    public static RelativeFilePath from(final String path) {
        return new RelativeFilePath(Paths.get(path));
    }

    public Path toJavaPath() {
        return path;
    }

    @Override
    public String toString() {
        return path.toString();
    }
}
