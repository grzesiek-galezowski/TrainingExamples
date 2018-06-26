package _07_picking_test_values;

public class BackupFileNamePattern {
    public String applyTo(String hostName, String userName) {
        return "backup_" + hostName + "_" + userName + ".zip";
    }
}
