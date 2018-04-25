package tailrecursion;

import io.vavr.collection.List;

import static java.lang.System.out;

public class _02_RecursiveLoopTailCallOptimizable {

    //if only we could write it like this:
    int foo(int n) {
        return foo(n + 1); //btw, infinite loop
    }

    //btw, Java does not have tail call recursion optimization


    public static void main(String[] args) {
        List<Integer> ints = List.of(1, 2, 3, 4, 5, 6, 7, 8);
        out.println(" INPUT:   " + ints);

        List<Integer> intsPlus1 = addOneToInfo(ints);
        out.println("OUTPUT:   " + intsPlus1);

        //todo show as single expression
    }

    private static List<Integer> addOneTo(List<Integer> input) {
        return addOneTo(input, List.empty() /* identity */);
    }

    private static List<Integer> addOneTo(
        List<Integer> input, List<Integer> aggregated) {

        if (input.isEmpty()) {
            return aggregated;
        }

        List<Integer> tail = input.tail();
        Integer head = input.head();

        List<Integer> transformedAggregated
            = aggregated.prepend(head + 1);

        return addOneTo(tail, transformedAggregated);
    }


    private static List<Integer> addOneToInfo(List<Integer> input) {
        return addOneToInfo(input, List.empty() /* identity */);
    }

    private static List<Integer> addOneToInfo(
        List<Integer> input, List<Integer> aggregated) {
        out.println("addOneTo(" + input + " , " + aggregated + ")");

        if (input.isEmpty()) {
            return aggregated;
        }

        final Integer head = input.head();
        final List<Integer> tail = input.tail();

        return addOneToInfo(tail, aggregated.append(head + 1));
    }

    private static List<Integer> addOneToAsExpression(
        List<Integer> input, List<Integer> aggregated) {

        return input.isEmpty() ? aggregated :
            addOneTo(input.tail(), aggregated.prepend(input.head() + 1));

    }


}
