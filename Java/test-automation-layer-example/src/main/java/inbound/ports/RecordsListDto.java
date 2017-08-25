package inbound.ports;

import lombok.Builder;

import java.util.List;

@Builder
public class RecordsListDto {
  private String artist;
  private List<RecordDto> records;

}
