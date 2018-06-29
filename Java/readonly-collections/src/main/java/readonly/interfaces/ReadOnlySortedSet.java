package readonly.interfaces;

import java.util.Comparator;

public interface ReadOnlySortedSet< T> extends ReadOnlySet<T> {
    Comparator<? super T> comparator();

    ReadOnlySortedSet<T> subSet(T var1, T var2);

    ReadOnlySortedSet<T> headSet(T var1);

    ReadOnlySortedSet<T> tailSet(T var1);

    T first();

    T last();
}

