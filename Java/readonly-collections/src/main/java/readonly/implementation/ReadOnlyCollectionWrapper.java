package readonly.implementation;

import readonly.implementation.iterator.ReadOnlyCollectionIteratorWrapper;
import readonly.interfaces.ReadOnlyCollection;
import readonly.interfaces.ReadOnlyCollectionIterator;

import java.io.Serializable;
import java.util.Collection;
import java.util.Spliterator;
import java.util.function.IntFunction;
import java.util.stream.Stream;

public class ReadOnlyCollectionWrapper<T> implements ReadOnlyCollection<T>, Serializable {
    private final Collection<T> original;

    public ReadOnlyCollectionWrapper(final Collection<T> original) {
        this.original = original;
    }

    @Override
    public int size() {
        return original.size();
    }

    @Override
    public boolean isEmpty() {
        return original.isEmpty();
    }

    @Override
    public boolean contains(Object o) {
        return original.contains(o);
    }

    @Override
    public ReadOnlyCollectionIterator<T> iterator() {
        return new ReadOnlyCollectionIteratorWrapper<T>(original.iterator());
    }

    @Override
    public Object[] toArray() {
        return original.toArray();
    }

    @Override
    public <T1> T1[] toArray(T1[] a) {
        return original.toArray(a);
    }

    @Override
    public <T1> T1[] toArray(IntFunction<T1[]> intFunction) {
        return original.stream().toArray(intFunction);
    }

    @Override
    public boolean containsAll(Collection<?> c) {
        return original.containsAll(c);
    }

    @Override
    public Spliterator<T> spliterator() {
        return original.spliterator();
    }

    @Override
    public Stream<T> stream() {
        return original.stream();
    }

    @Override
    public Stream<T> parallelStream() {
        return original.parallelStream();
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) {
            return true;
        }
        if (o == null || getClass() != o.getClass()) {
            return false;
        }

        ReadOnlyCollectionWrapper<?> that = (ReadOnlyCollectionWrapper<?>) o;

        return original != null ? original.equals(that.original) : that.original == null;
    }

    @Override
    public int hashCode() {
        return original != null ? original.hashCode() : 0;
    }
}
