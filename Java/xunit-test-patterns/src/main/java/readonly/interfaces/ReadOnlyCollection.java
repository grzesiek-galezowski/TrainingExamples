package readonly.interfaces;

import java.util.Collection;
import java.util.Spliterator;
import java.util.stream.Stream;

public interface ReadOnlyCollection<T> {
    int size();

    boolean isEmpty();

    boolean contains(Object o);

    ReadOnlyCollectionIterator<T> iterator();

    Object[] toArray();

    <T1> T1[] toArray(T1[] a);

    boolean containsAll(Collection<?> c);

    Spliterator<T> spliterator();

    Stream<T> stream();

    Stream<T> parallelStream();
}
