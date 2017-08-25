package inbound.ports;

import lombok.Builder;

@Builder
public class RecordDto {
  private String artist;
  private String title;
}
