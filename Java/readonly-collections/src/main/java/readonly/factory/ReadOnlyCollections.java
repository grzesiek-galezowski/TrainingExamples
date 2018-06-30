package readonly.factory;

import readonly.implementation.ReadOnlyCollectionWrapper;
import readonly.implementation.ReadOnlyDequeWrapper;
import readonly.implementation.ReadOnlyListWrapper;
import readonly.implementation.ReadOnlyMapWrapper;
import readonly.implementation.ReadOnlyNavigableMapWrapper;
import readonly.implementation.ReadOnlyNavigableSetWrapper;
import readonly.implementation.ReadOnlyQueueWrapper;
import readonly.implementation.ReadOnlySetWrapper;
import readonly.implementation.ReadOnlySortedMapWrapper;
import readonly.implementation.ReadOnlySortedSetWrapper;
import readonly.interfaces.ReadOnlyCollection;
import readonly.interfaces.ReadOnlyDeque;
import readonly.interfaces.ReadOnlyList;
import readonly.interfaces.ReadOnlyMap;
import readonly.interfaces.ReadOnlyNavigableMap;
import readonly.interfaces.ReadOnlyNavigableSet;
import readonly.interfaces.ReadOnlyQueue;
import readonly.interfaces.ReadOnlySet;
import readonly.interfaces.ReadOnlySortedMap;
import readonly.interfaces.ReadOnlySortedSet;

import java.util.Collection;
import java.util.Deque;
import java.util.List;
import java.util.Map;
import java.util.NavigableMap;
import java.util.NavigableSet;
import java.util.Queue;
import java.util.Set;
import java.util.SortedMap;
import java.util.SortedSet;

public class ReadOnlyCollections {
    public <T> ReadOnlyCollection<T> readOnlyCollection(Collection<T> collection) {
        return new ReadOnlyCollectionWrapper<>(collection);
    }

    public <T> ReadOnlyList<T> readOnlyList(List<T> list) {
        return new ReadOnlyListWrapper<>(list);
    }

    public <T> ReadOnlySet<T> readOnlySet(Set<T> set) {
        return new ReadOnlySetWrapper<>(set);
    }

    public <T> ReadOnlyQueue<T> readOnlyQueue(Queue<T> queue) {
        return new ReadOnlyQueueWrapper<>(queue);
    }

    public <T> ReadOnlyDeque<T> readOnlyDeque(Deque<T> deque) {
        return new ReadOnlyDequeWrapper<>(deque);
    }

    public <T> ReadOnlyNavigableSet<T> readOnlyNavigableSet(NavigableSet<T> navigableSet) {
        return new ReadOnlyNavigableSetWrapper<>(navigableSet);
    }

    public <T> ReadOnlySortedSet<T> readOnlySortedSet(SortedSet<T> sortedSet) {
        return new ReadOnlySortedSetWrapper<>(sortedSet);
    }

    public <K,V> ReadOnlyMap<K,V> readOnlyMap(Map<K, V> map) {
        return new ReadOnlyMapWrapper<>(map);
    }
    public <K,V> ReadOnlySortedMap<K,V> readOnlySortedMap(SortedMap<K, V> sortedMap) {
        return new ReadOnlySortedMapWrapper<>(sortedMap);
    }
    public <K,V> ReadOnlyNavigableMap<K,V> readOnlyNavigableMap(NavigableMap<K, V> navigableMap) {
        return new ReadOnlyNavigableMapWrapper<>(navigableMap);
    }
}
