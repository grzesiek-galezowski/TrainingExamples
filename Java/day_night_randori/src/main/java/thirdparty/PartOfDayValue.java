package thirdparty;

public enum PartOfDayValue { //resolution:
    MORNING,   //  0:00:00:00:01-12:00, 12:00 is still considered morning
    AFTERNOON, // 12:00:00:00:01-17:00, 17:00 is still considered afternoon
    EVENING,   // 17:00:00:00:01-21:00, 21:00 is still considered evening
    NIGHT      // 21:00:00:00:01-24:00, 24:00 is still considered night
}
