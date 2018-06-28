package readonly;

import readonly.interfaces.ReadOnlyCollectionIterator;

import java.util.Iterator;
import java.util.function.Consumer;

public class ReadOnlyCollectionIteratorWrapper<T> implements ReadOnlyCollectionIterator<T> {
    private Iterator<T> iterator;

    public ReadOnlyCollectionIteratorWrapper(Iterator<T> iterator) {
        this.iterator = iterator;
    }

    @Override
    public boolean hasNext() {
        return iterator.hasNext();
    }

    @Override
    public T next() {
        return iterator.next();
    }

    @Override
    public void forEachRemaining(Consumer<? super T> action) {
        iterator.forEachRemaining(action);
    }
}
