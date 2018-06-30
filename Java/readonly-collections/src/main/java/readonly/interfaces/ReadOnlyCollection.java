package readonly.interfaces;

import java.util.Collection;
import java.util.Spliterator;
import java.util.function.IntFunction;
import java.util.stream.Stream;

public interface ReadOnlyCollection<T> {
    int size();

    boolean isEmpty();

    boolean contains(Object o);

    boolean containsAll(Collection<?> c);

    ReadOnlyCollectionIterator<T> iterator();

    Object[] toArray();

    <T1> T1[] toArray(T1[] a);

    <T1> T1[] toArray(IntFunction<T1[]> intFunction);

    Spliterator<T> spliterator();

    Stream<T> stream();

    Stream<T> parallelStream();
}
