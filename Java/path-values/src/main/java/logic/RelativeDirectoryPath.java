package logic;

import lombok.EqualsAndHashCode;

import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.Optional;

@EqualsAndHashCode
public final class RelativeDirectoryPath {
    private final Path path;

    public RelativeDirectoryPath(final Path path) {
        this.path = path;
    }

    public static RelativeDirectoryPath from(final String path) {
        return new RelativeDirectoryPath(Paths.get(path));
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
}
