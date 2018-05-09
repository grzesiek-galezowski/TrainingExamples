package analysis;

import io.vavr.Function2;
import io.vavr.Tuple;
import io.vavr.Tuple2;

import java.util.HashMap;
import java.util.Map;

public class _04_Memoization {
    public static HashMap<Tuple2<Integer, Integer>, Integer> cache = new HashMap<>();

    public static void main(String[] args) {

        System.out.println("1 waiting");
        System.out.println("Result: " +
            Memoized.of(cache, Tuple.of(12, 13), _04_Memoization::add));
        System.out.println("1 finished");

        System.out.println("2 waiting");
        System.out.println("Result: " +
            Memoized.of(cache, Tuple.of(12, 13), _04_Memoization::add));
        System.out.println("2 finished");

        System.out.println("3 waiting");
        System.out.println("Result: " +
            Memoized.of(cache, Tuple.of(12, 13), _04_Memoization::add));
        System.out.println("3 finished");
    }


    public static int add(int a, int b) {
        sleep();
        System.out.println("F");
        return a+b;
    }

    private static void sleep() {
        try {
            Thread.sleep(2000);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    interface Memoized {
        static <T1, T2, R> R of(
            Map<Tuple2<T1, T2>, R> cache,
            Tuple2<T1, T2> key, Function2<T1, T2, R> tupled) {
            synchronized (cache) {
                if (cache.containsKey(key)) {
                    return cache.get(key);
                } else {
                    final R value = tupled.apply(key._1, key._2);
                    cache.put(key, value);
                    return value;
                }
            }
        }
    }


}
