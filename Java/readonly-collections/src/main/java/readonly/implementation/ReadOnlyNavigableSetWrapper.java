package readonly.implementation;

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
}
