package logic;

import lombok.EqualsAndHashCode;
import lombok.NonNull;

import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.Optional;

@EqualsAndHashCode
public final class RelativeDirectoryPath {
    private final Path path;

    public RelativeDirectoryPath(final Path path) {
        this.path = path;
    }


    public static RelativeDirectoryPath from(@NonNull final String pathString) {
        if("".equals(pathString)) {
            throw new IllegalArgumentException("path cannot be an empty string");
        }

        Path path = Paths.get(pathString);
        if(path.isAbsolute()) {
            throw new IllegalArgumentException(pathString + " is an absolute path, but expected a relative path");
        }
        return new RelativeDirectoryPath(path);
    }

    @Override
    public String toString() {
        return path.toString();
    }

    public Path toJavaPath() {
        return path;
    }

    public RelativeDirectoryPath with(DirectoryName dirName) {
        return new RelativeDirectoryPath(
            path.resolve(dirName.toJavaPath()));
    }


    public RelativeFilePath with(FileName fileName) {
        return new RelativeFilePath(
            path.resolve(fileName.toString())
        );
    }

    public RelativeDirectoryPath with(RelativeDirectoryPath relativeDir2) {
        return new RelativeDirectoryPath(
            path.resolve(relativeDir2.toJavaPath())
        );
    }

    public RelativeFilePath with(RelativeFilePath relativePathWithFileName) {
        return new RelativeFilePath(
            path.resolve(relativePathWithFileName.toJavaPath())
        );
    }

    Optional<RelativeDirectoryPath> parent() {
        return Optional.ofNullable(path.getParent())
            .map(p -> new RelativeDirectoryPath(p));
    }

    DirectoryName directoryName() {
        return new DirectoryName(path.getFileName());
    }
}
