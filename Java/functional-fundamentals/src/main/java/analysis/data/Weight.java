package analysis.data;

import lombok.AllArgsConstructor;
import lombok.Value;

@AllArgsConstructor
@Value
public class Weight {
    public double amount;
    public String unit;
}
