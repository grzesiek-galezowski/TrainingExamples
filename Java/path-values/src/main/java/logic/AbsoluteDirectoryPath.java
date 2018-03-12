package logic;

import lombok.EqualsAndHashCode;
import lombok.NonNull;

import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.Optional;

@EqualsAndHashCode
public class AbsoluteDirectoryPath {

    private final Path path;

    AbsoluteDirectoryPath(Path path) {
        this.path = path;
    }

    public static AbsoluteDirectoryPath from(@NonNull String fromPath) {
        if("".equals(fromPath)) {
            throw new IllegalArgumentException("fromPath cannot be empty");
        }
        return new AbsoluteDirectoryPath(Paths.get(fromPath));
    }

    @Override
    public String toString() {
        return path.toString();
    }

    public AbsoluteFilePath with(FileName fileName) {
        //todo refactor?
        return new AbsoluteFilePath(Paths.get(this.path.toString(), fileName.toString()));
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
        if(path.equals(path.getRoot())) {
            return DirectoryName.from(path);
        }
        return DirectoryName.from(path.getFileName());
    }

    public AbsoluteDirectoryPath with(DirectoryName subdir2) {
        return new AbsoluteDirectoryPath(path.resolve(subdir2.toString()));
    }
}
