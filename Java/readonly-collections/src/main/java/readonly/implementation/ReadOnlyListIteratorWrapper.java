package readonly.implementation;

import readonly.interfaces.ReadOnlyListIterator;

import java.util.ListIterator;

public class ReadOnlyListIteratorWrapper<T>
    extends ReadOnlyCollectionIteratorWrapper<T>
    implements ReadOnlyListIterator<T> {

    private final ListIterator<T> iterator;

    public ReadOnlyListIteratorWrapper(final ListIterator<T> iterator) {
        super(iterator);
        this.iterator = iterator;
    }

    @Override
    public boolean hasPrevious() {
        return iterator.hasPrevious();
    }

    @Override
    public T previous() {
        return iterator.previous();
    }

    @Override
    public int nextIndex() {
        return iterator.nextIndex();
    }

    @Override
    public int previousIndex() {
        return iterator.previousIndex();
    }
}
