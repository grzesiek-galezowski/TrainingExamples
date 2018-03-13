package logic;

import lombok.EqualsAndHashCode;
import lombok.NonNull;
import lombok.val;

import java.nio.file.Path;
import java.nio.file.Paths;

@EqualsAndHashCode()
public final class AbsoluteFilePath {
    private final Path path;

    AbsoluteFilePath(final Path path) {
        this.path = path;
    }

    @Override
    public String toString() {
        return path.toString();
    }

    public static AbsoluteFilePath from(@NonNull final String pathString) {
        val path = Paths.get(pathString);
        if(!path.isAbsolute()) {
            throw new IllegalArgumentException(pathString + " is not an absolute file path");
        }
        if(path.getRoot().equals(path)) {
            throw new IllegalArgumentException(pathString + "is a root path, an absolute file path");
        }
        return new AbsoluteFilePath(path);
    }

    public AbsoluteDirectoryPath parent() {
        return new AbsoluteDirectoryPath(path.getParent());
    }

    public FileName fileName() {
        return new FileName(path.getFileName());
    }

    public Path toJavaPath() {
        return path;
    }

    public AbsoluteDirectoryPath root() {
        return new AbsoluteDirectoryPath(path.getRoot());
    }
}
