package analysis;

import lombok.AllArgsConstructor;
import lombok.Value;

import java.util.Locale;

@AllArgsConstructor
@Value
class BookInfo {
    private int pages;
    private PublishInfo publishInfo;
    private Locale locale;
    private String isbn10;
    private String isbn13;
    private Dimensions productDimensions;
    private Weight shippingWeight;

}
