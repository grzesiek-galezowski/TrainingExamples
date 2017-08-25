//add records - admin
//navigate - by artist, by genre
//listen to records for some time
//generate reports how much money is given to each artist

import application.MusicStreamingService;
import inbound.ports.RecordDto;
import inbound.ports.RecordsListDto;
import lombok.val;
import org.junit.Test;

import java.util.Arrays;

import static org.assertj.core.api.Assertions.assertThat;

public class BehaviorsSpecification {

  @Test
  public void shouldShowUsersAllAddedRecordsByArtist() {
    MusicStreamingService service = new MusicStreamingService();

    RecordDto record1 = RecordDto.builder().artist("Prodigy").title("Experience").build();
    RecordDto record2 = RecordDto.builder().artist("Prodigy").title("Music for the Jilted Generation").build();
    service.addRecord(record1);
    service.addRecord(record2);

    val records = service.getRecordsByArtist("Prodigy");

    val expectedRecords = RecordsListDto.builder().artist("Prodigy")
        .records(Arrays.asList(
            record1,
            record2
        ));

    assertThat(records).isEqualToComparingFieldByFieldRecursively(expectedRecords);
  }

}
