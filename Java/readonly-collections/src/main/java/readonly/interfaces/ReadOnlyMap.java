package readonly.interfaces;

import java.util.Map;
import java.util.function.BiConsumer;

public interface ReadOnlyMap<K,V> {
    int size();
    boolean isEmpty();
    boolean containsKey(Object key);
    boolean containsValue(Object value);
    V get(Object key);
    ReadOnlySet<K> keySet();
    ReadOnlyCollection<V> values();
    ReadOnlySet<Map.Entry<K, V>> entrySet();
    boolean equals(Object o);
    int hashCode();
    V getOrDefault(Object key, V defaultValue);
    void forEach(BiConsumer<? super K, ? super V> action);
}

