package application;

import inbound.ports.Api;
import inbound.ports.RecordDto;
import inbound.ports.RecordsListDto;

public class MusicStreamingService implements Api {

  @Override
  public RecordsListDto getRecordsByArtist(final String artist) {
    return null;
  }

  @Override
  public void addRecord(final RecordDto recordDto) {

  }
}
