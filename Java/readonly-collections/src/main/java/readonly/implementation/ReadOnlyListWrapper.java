package readonly.implementation;

import readonly.implementation.iterator.ReadOnlyListIteratorWrapper;
import readonly.interfaces.ReadOnlyList;
import readonly.interfaces.ReadOnlyListIterator;

import java.io.Serializable;
import java.util.List;
import java.util.function.Consumer;

public class ReadOnlyListWrapper<T>
    extends ReadOnlyCollectionWrapper<T>
    implements ReadOnlyList<T>, Serializable {
    private final List<T> original;

    public ReadOnlyListWrapper(final List<T> list) {
        super(list);
        original = list;
    }

    @Override
    public T get(int index) {
        return original.get(index);
    }

    @Override
    public int indexOf(Object o) {
        return original.indexOf(o);
    }

    @Override
    public int lastIndexOf(Object o) {
        return original.lastIndexOf(o);
    }

    @Override
    public ReadOnlyListIterator<T> listIterator() {
        return new ReadOnlyListIteratorWrapper<>(original.listIterator());
    }

    @Override
    public ReadOnlyListIterator<T> listIterator(int index) {
        return new ReadOnlyListIteratorWrapper<>(original.listIterator(index));
    }

    @Override
    public List<T> subList(int fromIndex, int toIndex) {
        return original.subList(fromIndex, toIndex);
    }

    @Override
    public void forEach(Consumer<? super T> action) {
        original.forEach(action);
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

        ReadOnlyListWrapper<?> that = (ReadOnlyListWrapper<?>) o;

        return original != null ? original.equals(that.original) : that.original == null;
    }

    @Override
    public int hashCode() {
        int result = super.hashCode();
        result = 31 * result + (original != null ? original.hashCode() : 0);
        return result;
    }
}
