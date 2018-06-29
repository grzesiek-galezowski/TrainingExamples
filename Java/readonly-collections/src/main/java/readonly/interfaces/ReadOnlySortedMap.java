package readonly.interfaces;

import java.util.Comparator;

public interface ReadOnlySortedMap<K,V> extends ReadOnlyMap<K,V> {
    Comparator<? super K> comparator();
    ReadOnlySortedMap<K,V> subMap(K fromKey, K toKey);
    ReadOnlySortedMap<K,V> headMap(K toKey);
    ReadOnlySortedMap<K,V> tailMap(K fromKey);
    K firstKey();
    K lastKey();
}

