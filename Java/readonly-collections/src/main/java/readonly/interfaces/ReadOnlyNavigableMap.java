package readonly.interfaces;

import java.util.Map;

public interface ReadOnlyNavigableMap<K, V> extends ReadOnlySortedMap<K,V> {
    Map.Entry<K,V> lowerEntry(K key);
    K lowerKey(K key);
    Map.Entry<K,V> floorEntry(K key);
    K floorKey(K key);
    Map.Entry<K,V> ceilingEntry(K key);
    K ceilingKey(K key);
    Map.Entry<K,V> higherEntry(K key);
    K higherKey(K key);
    Map.Entry<K,V> firstEntry();
    Map.Entry<K,V> lastEntry();
    ReadOnlyNavigableMap<K,V> descendingMap();
    ReadOnlyNavigableSet<K> navigableKeySet();
    ReadOnlyNavigableSet<K> descendingKeySet();
    ReadOnlyNavigableMap<K,V> subMap(K fromKey, boolean fromInclusive,
                             K toKey,   boolean toInclusive);
    ReadOnlyNavigableMap<K,V> headMap(K toKey, boolean inclusive);
    ReadOnlyNavigableMap<K,V> tailMap(K fromKey, boolean inclusive);
}
