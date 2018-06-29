package readonly.interfaces;

public interface ReadOnlyDequeue<T> extends ReadOnlyQueue<T> {
    T getFirst();

    T getLast();

    T peekFirst();

    T peekLast();

    ReadOnlyCollectionIterator<T> descendingIterator();

}
