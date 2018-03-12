package logic;

import java.nio.file.Path;

public class AbsoluteFilePath {
    private Path path;

    public AbsoluteFilePath(Path path) {

        this.path = path;
    }

    @Override
    public String toString() {
        return path.toString();
    }
}
