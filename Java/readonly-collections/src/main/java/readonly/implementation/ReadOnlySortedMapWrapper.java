package readonly.implementation;

import readonly.interfaces.ReadOnlySortedMap;

import java.util.Comparator;
import java.util.SortedMap;

public class ReadOnlySortedMapWrapper<K, V>
    extends ReadOnlyMapWrapper<K, V>
    implements ReadOnlySortedMap<K, V> {

    private SortedMap<K, V> original;

    public ReadOnlySortedMapWrapper(SortedMap<K, V> original) {
        super(original);
        this.original = original;
    }

    @Override
    public Comparator<? super K> comparator() {
        return original.comparator();
    }

    @Override
    public ReadOnlySortedMap<K, V> subMap(K fromKey, K toKey) {
        return new ReadOnlySortedMapWrapper<>(original.subMap(fromKey, toKey));
    }

    @Override
    public ReadOnlySortedMap<K, V> headMap(K toKey) {
        return new ReadOnlySortedMapWrapper<>(original.headMap(toKey));
    }

    @Override
    public ReadOnlySortedMap<K, V> tailMap(K fromKey) {
        return new ReadOnlySortedMapWrapper<>(original.tailMap(fromKey));
    }

    @Override
    public K firstKey() {
        return original.firstKey();
    }

    @Override
    public K lastKey() {
        return original.lastKey();
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

        ReadOnlySortedMapWrapper<?, ?> that = (ReadOnlySortedMapWrapper<?, ?>) o;

        return original != null ? original.equals(that.original) : that.original == null;
    }

    @Override
    public int hashCode() {
        int result = super.hashCode();
        result = 31 * result + (original != null ? original.hashCode() : 0);
        return result;
    }
}
