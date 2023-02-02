namespace Application.Ports;

public record TrackDto(int Bars, int DalSegnoBar, TrackEntryDto[] Entries);