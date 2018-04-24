package tailrecursion;

import io.vavr.collection.List;

import static java.lang.System.out;

public class _01_RecursiveLoop {
    public static void main(String[] args) {
        List<Integer> ints = List.of(1, 2, 3, 4, 5, 6, 7, 8);
        out.println(ints);
        List<Integer> intsPlus1_1 = addOneToInfo(ints);
        out.println(intsPlus1_1);
    }

    //TAIL NON-RECURSIVE
    private static List<Integer> addOneTo(List<Integer> input) {
        if (input.isEmpty()) {
            return input;
        }

        final Integer head = input.head();
        final List<Integer> tail = input.tail();
        final Integer transformedHead = head + 1;
        return addOneTo(tail).prepend(transformedHead);
    }

    private static List<Integer> addOneTo2(List<Integer> input) {
        if (input.isEmpty()) {
            return input;
        }

        return addOneTo2(input.tail()).prepend(input.head() + 1);
    }

    private static List<Integer> addOneToInfo(List<Integer> input) {
        if (input.isEmpty()) {
            out.println("(EMPTY)");
            return input;
        }

        final Integer head = input.head();
        final List<Integer> tail = input.tail();
        out.println("(" + head + " + " + 1 + ") + addOne(" + tail + ")");

        final Integer transformedHead = head + 1;
        List<Integer> transformedTail = addOneToInfo(tail);
        return transformedTail.prepend(transformedHead);
    }

}
