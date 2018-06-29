package readonly.implementation;

import readonly.implementation.iterator.ReadOnlyCollectionIteratorWrapper;
import readonly.interfaces.ReadOnlyCollectionIterator;
import readonly.interfaces.ReadOnlyNavigableSet;

import java.io.Serializable;
import java.util.NavigableSet;

public class ReadOnlyNavigableSetWrapper<E>
    extends ReadOnlySortedSetWrapper<E>
    implements ReadOnlyNavigableSet<E>, Serializable {

    private final NavigableSet<E> original;

    public ReadOnlyNavigableSetWrapper(NavigableSet<E> original) {
        super(original);
        this.original = original;
    }

    @Override
    public E lower(E var1) {
        return original.lower(var1);
    }

    @Override
    public E floor(E var1) {
        return original.floor(var1);
    }

    @Override
    public E ceiling(E var1) {
        return original.ceiling(var1);
    }

    @Override
    public E higher(E var1) {
        return original.higher(var1);
    }

    @Override
    public ReadOnlyNavigableSet<E> descendingSet() {
        return new ReadOnlyNavigableSetWrapper<>(original.descendingSet());
    }

    @Override
    public ReadOnlyCollectionIterator<E> descendingIterator() {
        return new ReadOnlyCollectionIteratorWrapper<>(original.descendingIterator());
    }

    @Override
    public ReadOnlyNavigableSet<E> subSet(E var1, boolean var2, E var3, boolean var4) {
        return new ReadOnlyNavigableSetWrapper<>(original.subSet(var1, var2, var3, var4));
    }

    @Override
    public ReadOnlyNavigableSet<E> headSet(E var1, boolean var2) {
        return new ReadOnlyNavigableSetWrapper<>(original.headSet(var1, var2));
    }

    @Override
    public ReadOnlyNavigableSet<E> tailSet(E var1, boolean var2) {
        return new ReadOnlyNavigableSetWrapper<>(original.tailSet(var1, var2));
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

        ReadOnlyNavigableSetWrapper<?> that = (ReadOnlyNavigableSetWrapper<?>) o;

        return original != null ? original.equals(that.original) : that.original == null;
    }

    @Override
    public int hashCode() {
        int result = super.hashCode();
        result = 31 * result + (original != null ? original.hashCode() : 0);
        return result;
    }
}
