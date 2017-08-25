package inbound.ports;

public interface Api {

  RecordsListDto getRecordsByArtist(String artist);

  void addRecord(RecordDto recordDto);
}
