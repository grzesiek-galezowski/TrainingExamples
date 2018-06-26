package _07_picking_test_values;

import autofixture.publicinterface.Any;
import lombok.val;
import org.testng.annotations.Test;

import static org.assertj.core.api.Assertions.assertThat;

public class _04_DerivedValues {

    @Test
    public void
    shouldCreateBackupFileNameContainingPassedHostNameAndUserName() {
        //GIVEN
        val hostName = Any.string();
        val userName = Any.string();
        val backupNamePattern = new BackupFileNamePattern();

        //WHEN
        val name = backupNamePattern.applyTo(hostName, userName);

        //THEN
        assertThat(name)
            .isEqualTo("backup_" + hostName + "_" + userName + ".zip");
    }
    //todo show extracting helper method
}
