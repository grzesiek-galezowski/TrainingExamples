package logic;

import org.testng.annotations.Test;

import static org.assertj.core.api.Assertions.assertThat;

public class IntegrationSpecification {
    @Test
    public void integrationExample() {
        AbsoluteFilePath with = AbsoluteDirectoryPath.from("C:\\")
            .with(RelativeDirectoryPath.from("Lolek\\lolki"))
            .with(RelativeDirectoryPath.from("Zenki"))
            .with(FileName.from("Zenek.txt"));

        assertThat(with.toString())
            .isEqualTo("C:\\Lolek\\lolki\\Zenki\\Zenek.txt");

    }
}
