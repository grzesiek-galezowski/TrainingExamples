package analysis;

import lombok.val;

public class _01_TypicalFunctions {

    public static void main(String[] args) {

        String str = "Ewa";

        val result = firstN(4, rplc("psa", "kota", apnd(".", apnd("psa", apnd(" ", apnd("ma", apnd(" ", str)))))));

        System.out.println(result);
    }

    private static String firstN(int i, String rplc) {
        return rplc.substring(0,i);

    }

    private static String apnd(String s2, String s1) {
        return s1 + s2;
    }

    private static String rplc(String pattern, String newText, String s1) {
        return s1.replace(pattern, newText);
    }
}
