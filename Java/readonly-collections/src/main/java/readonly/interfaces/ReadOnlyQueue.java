package readonly.interfaces;

public interface ReadOnlyQueue<T> extends ReadOnlyCollection<T> {
    T element();

    T peek();
}



