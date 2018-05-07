package analysis;

import lombok.AllArgsConstructor;
import lombok.Value;

@AllArgsConstructor
@Value
public class Dimensions {
    double width;
    double height;
    double depth;
    String unit;
}
