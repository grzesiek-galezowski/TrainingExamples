package readonly.implementation;

import readonly.interfaces.ReadOnlyQueue;

import java.io.Serializable;
import java.util.Queue;

public class ReadOnlyQueueWrapper<T>
    extends ReadOnlyCollectionWrapper<T>
    implements ReadOnlyQueue<T>, Serializable {

    private final Queue<T> original;

    public ReadOnlyQueueWrapper(Queue<T> original) {
        super(original);
        this.original = original;
    }

    @Override
    public T element() {
        return original.element();
    }

    @Override
    public T peek() {
        return original.peek();
    }
}
