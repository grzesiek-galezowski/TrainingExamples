package logic;

import lombok.EqualsAndHashCode;
import lombok.NonNull;

import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.Optional;

@EqualsAndHashCode
public final class RelativeFilePath {
    private final Path path;

    RelativeFilePath(final Path path) {
        this.path = path;
    }

    public static RelativeFilePath from(@NonNull final String pathString) {
        if("".equals(pathString)) {
            throw new IllegalArgumentException("path cannot be an empty string");
        }

        Path path = Paths.get(pathString);
        if(path.isAbsolute()) {
            throw new IllegalArgumentException(pathString + " is an absolute path, but relative path was expected");
        }
        return new RelativeFilePath(path);
    }

    public Path toJavaPath() {
        return path;
    }

    @Override
    public String toString() {
        return path.toString();
    }

    public Optional<RelativeDirectoryPath> parent() {
        return Optional.ofNullable(path.getParent())
            .map(p -> new RelativeDirectoryPath(p));
    }

    public FileName fileName() {
        return new FileName(path.getFileName());
    }
}
