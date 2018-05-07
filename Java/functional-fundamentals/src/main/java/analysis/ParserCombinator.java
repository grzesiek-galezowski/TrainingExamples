package analysis;

import lombok.val;

import java.util.Locale;
import java.util.function.Function;

public class ParserCombinator {
    public static void main(String[] args) {
        //forward - backwards analysis using inline

        String fileContent =
            "Paperback: 240 pages\n" +
            "Publisher: Addison-Wesley Professional; 1 edition (November 18, 2002)\n" +
            "Language: English\n" +
            "ISBN-10: 0321146530\n" +
            "ISBN-13: 978-0321146533\n" +
            "Product Dimensions: 7.3 x 0.7 x 9.1 inches\n" +
            "Shipping Weight: 1.6 pounds";

        val value = parseBookInfo(fileContent,
            line1 -> header(line1, "Paperback: ", c -> parsePages(c)),
            line2 -> header(line2, "Publisher: ", c -> parsePublishInfo(c)),
            line3 -> header(line3, "Language: ", c -> parseLanguage(c)),
            line4 -> header(line4, "ISBN-10: ", c -> parseIsbn10(c)),
            line5 -> header(line5, "ISBN-13: ", c -> parseIsbn13(c)),
            line6 -> header(line6, "Product Dimensions: ", c -> parseDimensions(c)),
            line7 -> header(line7, "Shipping Weight: ", c -> parseWeight(c))
        );
    }

    private static <T> T header(
        String content,
        String header,
        Function<String, T> parser) {
        val withoutHeader = content.replace(header, "");
        return parser.apply(withoutHeader);
    }

    private static Weight parseWeight(String s) {
        val parts = s.split("\\s");
        return new Weight(Double.valueOf(parts[0]), parts[1]);
    }

    private static Dimensions parseDimensions(String s) {
        val parts = s.split("\\s");
        return new Dimensions(
            Double.parseDouble(parts[0]),
            Double.parseDouble(parts[2]),
            Double.parseDouble(parts[4]),
            parts[5]);
    }

    private static String parseIsbn13(String s) {
        return s;
    }

    private static String parseIsbn10(String s) {
        return s;
    }

    private static Locale parseLanguage(String s) {
        return Locale.forLanguageTag(s); //won't work
    }

    private static PublishInfo parsePublishInfo(String publishInfoLine) {
        val parts = publishInfoLine.split(";");
        return new PublishInfo(parts[0], parts[1]);
    }

    private static Integer parsePages(String s) {
        val words = s.split("\\s");
        return Integer.valueOf(words[0]);
    }

    public static BookInfo parseBookInfo(
        String content,
        Function<String, Integer> pagesParser,
        Function<String, PublishInfo> publishInfoParser,
        Function<String, Locale> localeParser,
        Function<String, String> isbn10Parser,
        Function<String, String> isbn13Parser,
        Function<String, Dimensions> dimensionsParser,
        Function<String, Weight> weightParser
    ) {
        String[] lines = content.split("\n");
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

