package _02_basics;

import java.util.Arrays;
import java.util.List;

import static java.util.stream.Collectors.toList;

public class UniqueFilter {
    public List<Integer> applyTo(int... args) {
        return Arrays.stream(args)
            .distinct()
            .boxed()
            .collect(toList());
    }

    public List<Integer> apply3To(int... args) {
        return Arrays.stream(args)
            .distinct()
            .boxed()
            .limit(3)
            .collect(toList());
    }
}
