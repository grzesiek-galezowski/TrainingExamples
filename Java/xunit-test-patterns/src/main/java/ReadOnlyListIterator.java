import java.util.ListIterator;

public class ReadOnlyListIterator<T> extends ReadOnlyCollectionIterator<T> {
    private final ListIterator<T> iterator;

    public ReadOnlyListIterator(ListIterator<T> iterator) {
        super(iterator);
        this.iterator = iterator;
    }

    public boolean hasPrevious() {
        return iterator.hasPrevious();
    }

    public T previous() {
        return iterator.previous();
    }

    public int nextIndex() {
        return iterator.nextIndex();
    }

    public int previousIndex() {
        return iterator.previousIndex();
    }
}
