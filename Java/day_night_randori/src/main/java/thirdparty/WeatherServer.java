package thirdparty;

import production.MutablePartOfDay;

public interface WeatherServer {
    void notifyOnPartOfDay(PartOfDayValue partOfDayValue);
    void notifyOnError(PartOfDayValue partOfDayValueWhenErrorOccured, MutablePartOfDay errorTime);
}
