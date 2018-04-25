package tailrecursion;

import io.vavr.Function1;
import io.vavr.collection.List;

import static java.lang.System.out;

public class _03_SimpleFunctionalLoop {
    public static void main(String[] args) {
        List<Integer> ints = List.of(1,2,3,4,5,6,7,8);

        out.println(ints);

        List<Integer> intsPlus1 = myMap(ints, i -> i + 1);
        out.println(intsPlus1);
    }

    //TAIL RECURSIVE
    private static List<Integer> myMap(List<Integer> input, Function1<Integer, Integer> transform) {
        return myMap(input, List.empty(), transform);
    }

    private static List<Integer> myMap(
        List<Integer> input,
        List<Integer> aggregated,
        Function1<Integer, Integer> transform) {

        if(input.isEmpty()) {
            return aggregated;
        }

        final Integer head = input.head();
        final List<Integer> tail = input.tail();

        return myMap(
            tail,
            aggregated.append(transform.apply(head)),
            transform);
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
