package logic;

import java.nio.file.Path;
import java.nio.file.Paths;

public class DirectoryName {
    private Path fileName;

    //todo hide!
    public static DirectoryName from(Path fileName) {
        return new DirectoryName(fileName);
    }

    public static DirectoryName from(String subdir) {
        return new DirectoryName(Paths.get(subdir));
    }

    DirectoryName(Path fileName) {

        this.fileName = fileName;
    }

    @Override
    public String toString() {
        return fileName.toString();
    }
}
