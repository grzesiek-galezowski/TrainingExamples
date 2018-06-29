package readonly.implementation;

import readonly.interfaces.ReadOnlyCollection;
import readonly.interfaces.ReadOnlyMap;
import readonly.interfaces.ReadOnlySet;

import java.io.Serializable;
import java.util.Map;
import java.util.function.BiConsumer;

public class ReadOnlyMapWrapper<K,V> implements ReadOnlyMap<K, V>, Serializable {

    private final Map<K,V> original;

    public ReadOnlyMapWrapper(Map<K, V> original) {
        this.original = original;
    }

    @Override
    public int size() {
        return original.size();
    }

    @Override
    public boolean isEmpty() {
        return original.isEmpty();
    }

    @Override
    public boolean containsKey(Object key) {
        return original.containsKey(key);
    }

    @Override
    public boolean containsValue(Object value) {
        return original.containsValue(value);
    }

    @Override
    public V get(Object key) {
        return original.get(key);
    }

    @Override
    public ReadOnlySet<K> keySet() {
        return new ReadOnlySetWrapper<>(original.keySet());
    }

    @Override
    public ReadOnlyCollection<V> values() {
        return new ReadOnlyCollectionWrapper<>(original.values());
    }

    @Override
    public ReadOnlySet<Map.Entry<K, V>> entrySet() {
        return new ReadOnlySetWrapper<>(original.entrySet());
    }

    @Override
    public V getOrDefault(Object key, V defaultValue) {
        return original.getOrDefault(key, defaultValue);
    }

    @Override
    public void forEach(BiConsumer<? super K, ? super V> action) {
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

        ReadOnlyMapWrapper<?, ?> that = (ReadOnlyMapWrapper<?, ?>) o;

        return original != null ? original.equals(that.original) : that.original == null;
    }

    @Override
    public int hashCode() {
        return original != null ? original.hashCode() : 0;
    }
}
