package _03_analysis.data;

//todo make these all final

import lombok.AllArgsConstructor;
import lombok.Value;

@AllArgsConstructor
@Value
public class PublishInfo {
    private final String publisher;
    private final String edition;
}
