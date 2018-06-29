package readonly.implementation;

import readonly.interfaces.ReadOnlyCollectionIterator;

import java.util.Iterator;
import java.util.function.Consumer;

public class ReadOnlyCollectionIteratorWrapper<T> implements ReadOnlyCollectionIterator<T> {
    private final Iterator<T> iterator;

    public ReadOnlyCollectionIteratorWrapper(final Iterator<T> iterator) {
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
