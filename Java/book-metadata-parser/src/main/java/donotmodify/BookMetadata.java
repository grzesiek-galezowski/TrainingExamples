package donotmodify;

import java.util.Locale;

public class BookMetadata {
    public int pages;
    public Publisher publisher;
    public Locale language; //use new Locale("...") to obtain
    public String isbn10;
    public String isbn13;
    public Dimensions dimensions;
    //public Weight shippingWeight;
}
