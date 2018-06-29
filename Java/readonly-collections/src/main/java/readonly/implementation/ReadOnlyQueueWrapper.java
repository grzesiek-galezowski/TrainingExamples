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

        ReadOnlyQueueWrapper<?> that = (ReadOnlyQueueWrapper<?>) o;

        return original != null ? original.equals(that.original) : that.original == null;
    }

    @Override
    public int hashCode() {
        int result = super.hashCode();
        result = 31 * result + (original != null ? original.hashCode() : 0);
        return result;
    }
}
