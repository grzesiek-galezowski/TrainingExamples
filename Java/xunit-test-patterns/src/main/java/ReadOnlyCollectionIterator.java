import java.util.Iterator;
import java.util.function.Consumer;

public class ReadOnlyCollectionIterator<T> {
    private Iterator<T> iterator;

    public ReadOnlyCollectionIterator(Iterator<T> iterator) {
        this.iterator = iterator;
    }

    public boolean hasNext() {
        return iterator.hasNext();
    }

    public T next() {
        return iterator.next();
    }

    public void forEachRemaining(Consumer<? super T> action) {
        iterator.forEachRemaining(action);
    }
}
