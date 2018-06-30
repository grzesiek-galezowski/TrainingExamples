package readonly.implementation;

import readonly.implementation.iterator.ReadOnlyCollectionIteratorWrapper;
import readonly.interfaces.ReadOnlyCollectionIterator;
import readonly.interfaces.ReadOnlyDeque;

import java.io.Serializable;
import java.util.Deque;

public class ReadOnlyDequeWrapper<T>
    extends ReadOnlyQueueWrapper<T>
    implements ReadOnlyDeque<T>, Serializable {

    private final Deque<T> original;

    public ReadOnlyDequeWrapper(final Deque<T> original) {
        super(original);
        this.original = original;
    }

    @Override
    public T getFirst() {
        return original.getFirst();
    }

    @Override
    public T getLast() {
        return original.getLast();
    }

    @Override
    public T peekFirst() {
        return original.peekFirst();
    }

    @Override
    public T peekLast() {
        return original.peekLast();
    }

    @Override
    public ReadOnlyCollectionIterator<T> descendingIterator() {
        return new ReadOnlyCollectionIteratorWrapper<>(original.descendingIterator());
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) {
            return true;
        }
        if (o == null || getClass() != o.getClass()) {
            return false;
        }
        if (!super.equals(o)) {
            return false;
        }

        ReadOnlyDequeWrapper<?> that = (ReadOnlyDequeWrapper<?>) o;

        return original != null ? original.equals(that.original) : that.original == null;
    }

    @Override
    public int hashCode() {
        int result = super.hashCode();
        result = 31 * result + (original != null ? original.hashCode() : 0);
        return result;
    }

}
