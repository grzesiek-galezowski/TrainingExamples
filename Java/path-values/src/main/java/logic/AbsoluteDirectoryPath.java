package logic;

import lombok.EqualsAndHashCode;
import lombok.NonNull;
import lombok.val;

import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.Optional;

@EqualsAndHashCode
public final class AbsoluteDirectoryPath {

    private final Path path;

    AbsoluteDirectoryPath(final Path path) {
        this.path = path;
    }

    public static AbsoluteDirectoryPath from(@NonNull final String fromPath) {
        val path = Paths.get(fromPath);
        if (!path.isAbsolute()) {
            throw new IllegalArgumentException(fromPath + "is not an absolute path");
        }
        return new AbsoluteDirectoryPath(path);

    }

    @Override
    public String toString() {
        return path.toString();
    }

    public AbsoluteFilePath with(final FileName fileName) {
        //todo refactor?
        return new AbsoluteFilePath(path.resolve(fileName.toString()));
    }

    public Path toJavaPath() {
        return path;
    }

    public AbsoluteDirectoryPath root() {
        return new AbsoluteDirectoryPath(path.getRoot());
    }

    public Optional<AbsoluteDirectoryPath> parent() {
        return Optional.ofNullable(path.getParent())
            .map(p -> new AbsoluteDirectoryPath(p));
    }

    public DirectoryName directoryName() {
        if (path.equals(path.getRoot())) {
            return new DirectoryName(path);
        }
        return new DirectoryName(path.getFileName());
    }

    public AbsoluteDirectoryPath with(
        final DirectoryName subdir2) {
        return new AbsoluteDirectoryPath(path.resolve(subdir2.toString()));
    }

    public AbsoluteDirectoryPath with(
        final RelativeDirectoryPath relativePath) {
        return new AbsoluteDirectoryPath(
            path.resolve(relativePath.toJavaPath()));
    }

    public AbsoluteFilePath with(
        final RelativeFilePath relativePath) {
        return new AbsoluteFilePath(
            path.resolve(relativePath.toJavaPath()));
    }
}
