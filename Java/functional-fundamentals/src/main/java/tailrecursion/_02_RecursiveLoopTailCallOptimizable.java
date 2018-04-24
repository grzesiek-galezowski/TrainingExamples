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
        List<Integer> ints = List.of(1,2,3,4,5,6,7,8);
        out.println(ints);

        List<Integer> intsPlus1 = addOneToInfo(ints);
        out.println(intsPlus1);
    }

    private static List<Integer> addOneTo(List<Integer> input) {
        return addOneTo(input, List.empty() /* identity */);
    }

    private static List<Integer> addOneTo(
        List<Integer> input, List<Integer> aggregated) {

        if(input.isEmpty()) {
            return aggregated;
        }

        return addOneTo(
            input.tail(),
            aggregated.append(input.head() + 1));
    }

    private static List<Integer> addOneToInfo(List<Integer> input) {
        return addOneToInfo(input, List.empty() /* identity */);
    }

    private static List<Integer> addOneToInfo(
        List<Integer> input, List<Integer> aggregated) {
        out.println("input | aggregated : " + input + " | " + aggregated);

        if(input.isEmpty()) {
            return aggregated;
        }

        final Integer head = input.head();
        final List<Integer> tail = input.tail();

        return addOneToInfo(tail, aggregated.append(head + 1));
    }


    /*

// A NON-tail-recursive function.  The function is not tail
// recursive because the value returned by fact(n-1) is used in
// fact(n) and call to fact(n-1) is not the last thing done by fact(n)
unsigned int fact(unsigned int n)
{
    if (n == 0) return 1;

    return n*fact(n-1);
}


// A tail recursive function to calculate factorial
unsigned factTR(unsigned int n, unsigned int a)
{
    if (n == 0)  return a;

    return factTR(n-1, n*a);
}

// A wrapper over factTR
unsigned int fact(unsigned int n)
{
   return factTR(n, 1);
}

     */

}
