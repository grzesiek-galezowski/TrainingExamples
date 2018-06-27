import java.util.Collection;
import java.util.Spliterator;
import java.util.stream.Stream;

public class ReadOnlyCollection<T> {
    private final Collection<T> original;

    public ReadOnlyCollection(Collection<T> original) {
        this.original = original;
    }

    public int size() {
        return original.size();
    }

    public boolean isEmpty() {
        return original.isEmpty();
    }

    public boolean contains(Object o) {
        return original.contains(o);
    }

    public ReadOnlyCollectionIterator<T> iterator() {
        return new ReadOnlyCollectionIterator<T>(original.iterator());
    }

    public Object[] toArray() {
        return original.toArray();
    }

    public <T1> T1[] toArray(T1[] a) {
        return original.toArray(a);
    }

    public boolean containsAll(Collection<?> c) {
        return original.containsAll(c);
    }

    @Override
    public boolean equals(Object o) {
        //todo compare to read only and normal collections
        return original.equals(o);
    }

    @Override
    public int hashCode() {
        return original.hashCode();
    }

    public Spliterator<T> spliterator() {
        return original.spliterator();
    }

    public Stream<T> stream() {
        return original.stream();
    }

    public Stream<T> parallelStream() {
        return original.parallelStream();
    }
}
