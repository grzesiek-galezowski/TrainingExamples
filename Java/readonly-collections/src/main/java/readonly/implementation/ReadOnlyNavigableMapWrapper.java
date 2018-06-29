package readonly.implementation;

import readonly.interfaces.ReadOnlyNavigableMap;
import readonly.interfaces.ReadOnlyNavigableSet;

import java.io.Serializable;
import java.util.Map;
import java.util.NavigableMap;

public class ReadOnlyNavigableMapWrapper<K, V>
    extends ReadOnlySortedMapWrapper<K, V>
    implements ReadOnlyNavigableMap<K, V>, Serializable {

    private NavigableMap<K, V> original;

    public ReadOnlyNavigableMapWrapper(NavigableMap<K, V> original) {
        super(original);
        this.original = original;
    }

    @Override
    public Map.Entry<K, V> lowerEntry(K key) {
        return original.lowerEntry(key);
    }

    @Override
    public K lowerKey(K key) {
        return original.lowerKey(key);
    }

    @Override
    public Map.Entry<K, V> floorEntry(K key) {
        return original.floorEntry(key);
    }

    @Override
    public K floorKey(K key) {
        return original.floorKey(key);
    }

    @Override
    public Map.Entry<K, V> ceilingEntry(K key) {

        return original.ceilingEntry(key);
    }

    @Override
    public K ceilingKey(K key) {
        return original.ceilingKey(key);
    }

    @Override
    public Map.Entry<K, V> higherEntry(K key) {
        return original.higherEntry(key);
    }

    @Override
    public K higherKey(K key) {
        return original.higherKey(key);
    }

    @Override
    public Map.Entry<K, V> firstEntry() {
        return original.firstEntry();
    }

    @Override
    public Map.Entry<K, V> lastEntry() {
        return original.lastEntry();
    }

    @Override
    public ReadOnlyNavigableMap<K, V> descendingMap() {
        return new ReadOnlyNavigableMapWrapper<>(original.descendingMap());
    }

    @Override
    public ReadOnlyNavigableSet<K> navigableKeySet() {
        return new ReadOnlyNavigableSetWrapper<>(original.navigableKeySet());
    }

    @Override
    public ReadOnlyNavigableSet<K> descendingKeySet() {
        return new ReadOnlyNavigableSetWrapper<>(original.descendingKeySet());
    }

    @Override
    public ReadOnlyNavigableMap<K, V> subMap(K fromKey, boolean fromInclusive, K toKey, boolean toInclusive) {
        return new ReadOnlyNavigableMapWrapper<>(original.subMap(fromKey, fromInclusive, toKey, toInclusive));
    }

    @Override
    public ReadOnlyNavigableMap<K, V> headMap(K toKey, boolean inclusive) {
        return new ReadOnlyNavigableMapWrapper<>(original.headMap(toKey, inclusive));
    }

    @Override
    public ReadOnlyNavigableMap<K, V> tailMap(K fromKey, boolean inclusive) {
        return new ReadOnlyNavigableMapWrapper<>(original.tailMap(fromKey, inclusive));
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

        ReadOnlyNavigableMapWrapper<?, ?> that = (ReadOnlyNavigableMapWrapper<?, ?>) o;

        return original != null ? original.equals(that.original) : that.original == null;
    }

    @Override
    public int hashCode() {
        int result = super.hashCode();
        result = 31 * result + (original != null ? original.hashCode() : 0);
        return result;
    }
}
