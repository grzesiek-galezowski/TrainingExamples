package logic;

import lombok.EqualsAndHashCode;
import lombok.NonNull;
import lombok.val;

import java.nio.file.Path;
import java.nio.file.Paths;

@EqualsAndHashCode
public final class FileName {

    private final Path path;

    FileName(final Path path) {
        this.path = path;
    }

    //todo what about empty string?
    public static FileName from(@NonNull final String name) {
        val path = Paths.get(name);
        if(!path.getFileName().equals(path)) {
            throw new IllegalArgumentException(name + " is not a file name");
        }
        return new FileName(path);
    }

    @Override
    public String toString() {
        return path.toString();
    }
    //todo add toJavaPath()?
}
