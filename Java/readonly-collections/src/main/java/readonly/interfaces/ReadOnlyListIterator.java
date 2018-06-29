package readonly.interfaces;

public interface ReadOnlyListIterator<T> extends ReadOnlyCollectionIterator<T> {
    boolean hasPrevious();

    T previous();

    int nextIndex();

    int previousIndex();
}
