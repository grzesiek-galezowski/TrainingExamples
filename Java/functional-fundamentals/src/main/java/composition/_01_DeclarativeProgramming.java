package composition;

import _03_analysis.data.BookInfo;
import _03_analysis.data.Dimensions;
import _03_analysis.data.PublishInfo;
import _03_analysis.data.Weight;
import lombok.val;

import java.util.Locale;
import java.util.function.Function;

public class _01_DeclarativeProgramming {

    private static final String PAPERBACK = "Paperback: ";
    private static final String PUBLISHER = "Publisher: ";
    private static final String LANGUAGE = "Language: ";
    private static final String ISBN_10 = "ISBN-10: ";
    private static final String ISBN_13 = "ISBN-13: ";
    private static final String PRODUCT_DIMENSIONS = "Product Dimensions: ";
    private static final String SHIPPING_WEIGHT = "Shipping Weight: ";

    public static void main(String[] args) {

        String fileContent =
                "Paperback: 240 pages\n" +
                "Publisher: Addison-Wesley Professional; 1 edition (November 18, 2002)\n" +
                "Language: English\n" +
                "ISBN-10: 0321146530\n" +
                "ISBN-13: 978-0321146533\n" +
                "Product Dimensions: 7.3 x 0.7 x 9.1 inches\n" +
                "Shipping Weight: 1.6 pounds";

        final BookInfo value = from(fileContent,
            line(PAPERBACK, pages()),
            line(PUBLISHER, publishInfo()),
            line(LANGUAGE, language()),
            line(ISBN_10, isbn10()),
            line(ISBN_13, isbn13()),
            line(PRODUCT_DIMENSIONS, dimensions()),
            line(SHIPPING_WEIGHT, weight())
        );

        System.out.println(value);
    }

    private static <T> Function<String, T> line(String header, Function<String, T> pages) {
        return line1 -> header(line1, header, pages);
    }

    private static Function<String, Weight> weight() {
        return c -> weight(c);
    }

    private static Function<String, Dimensions> dimensions() {
        return c -> dimensions(c);
    }

    private static Function<String, String> isbn13() {
        return c -> isbn13(c);
    }

    private static Function<String, String> isbn10() {
        return c -> isbn10(c);
    }

    private static Function<String, Locale> language() {
        return c -> language(c);
    }

    private static Function<String, PublishInfo> publishInfo() {
        return c -> publishInfo(c);
    }

    private static Function<String, Integer> pages() {
        return c -> pages(c);
    }

    private static <T> T header(
        String content,
        String header,
        Function<String, T> parser) {
        val headerless = content.replace(header, "");
        return parser.apply(headerless);
    }

    private static Weight weight(String s) {
        val parts = s.split("\\s");
        return new Weight(Double.valueOf(parts[0]), parts[1]);
    }

    private static Dimensions dimensions(String s) {
        val parts = s.split("\\s");
        return new Dimensions(
            Double.parseDouble(parts[0]),
            Double.parseDouble(parts[2]),
            Double.parseDouble(parts[4]),
            parts[5]);
    }

    private static String isbn13(String s) {
        return s;
    }

    private static String isbn10(String s) {
        return s;
    }

    private static Locale language(String s) {
        return Locale.forLanguageTag(s); //won't work
    }

    private static PublishInfo publishInfo(String publishInfoLine) {
        val parts = publishInfoLine.split(";");
        return new PublishInfo(parts[0].trim(), parts[1].trim());
    }

    private static Integer pages(String s) {
        val words = s.split("\\s");
        return Integer.valueOf(words[0]);
    }

    public static BookInfo from(
        String content,
        Function<String, Integer> pagesParser,
        Function<String, PublishInfo> publishInfoParser,
        Function<String, Locale> localeParser,
        Function<String, String> isbn10Parser,
        Function<String, String> isbn13Parser,
        Function<String, Dimensions> dimensionsParser,
        Function<String, Weight> weightParser
    ) {
        val lines = content.split("\n");
        val pages = pagesParser.apply(lines[0]);
        val publishInfo = publishInfoParser.apply(lines[1]);
        val locale = localeParser.apply(lines[2]);
        val isbn10 = isbn10Parser.apply(lines[3]);
        val isbn13 = isbn13Parser.apply(lines[4]);
        val dimensions = dimensionsParser.apply(lines[5]);
        val weight = weightParser.apply(lines[6]);

        return new BookInfo(
            pages,
            publishInfo,
            locale,
            isbn10,
            isbn13,
            dimensions,
            weight
        );
    }
}


/*
Paperback: 240 pages
Publisher: Addison-Wesley Professional; 1 edition (November 18, 2002)
Language: English
ISBN-10: 0321146530
ISBN-13: 978-0321146533
Product Dimensions: 7.3 x 0.7 x 9.1 inches
Shipping Weight: 1.6 pounds
 */

