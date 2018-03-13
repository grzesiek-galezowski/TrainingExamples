package logic;

import lombok.EqualsAndHashCode;
import lombok.NonNull;

import java.nio.file.Path;
import java.nio.file.Paths;

@EqualsAndHashCode
public final class DirectoryName {
    private final Path fileName;


    public static final DirectoryName from(@NonNull final String name) {
        if ("".equals(name)) {
            throw new IllegalArgumentException("Name cannot be an empty string");
        }
        Path dirName = Paths.get(name);
        if (!dirName.equals(dirName.getFileName())) {
            throw new IllegalArgumentException("Multiple path segments not allowed in " + dirName);
        }
        return new DirectoryName(dirName);

    }

    DirectoryName(final Path fileName) {
        this.fileName = fileName;
    }

    @Override
    public final String toString() {
        return fileName.toString();
    }

    Path toJavaPath() {
        return fileName;
    }
}
