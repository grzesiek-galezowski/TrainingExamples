package readonly.interfaces;

public interface ReadOnlyNavigableSet<E> extends ReadOnlySortedSet<E> {
    E lower(E var1);

    E floor(E var1);

    E ceiling(E var1);

    E higher(E var1);

    ReadOnlyNavigableSet<E> descendingSet();

    ReadOnlyCollectionIterator<E> descendingIterator();

    ReadOnlyNavigableSet<E> subSet(E var1, boolean var2, E var3, boolean var4);

    ReadOnlyNavigableSet<E> headSet(E var1, boolean var2);

    ReadOnlyNavigableSet<E> tailSet(E var1, boolean var2);
}
