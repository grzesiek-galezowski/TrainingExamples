package readonly.interfaces;

import java.util.List;
import java.util.function.Consumer;

public interface ReadOnlyList<T> extends ReadOnlyCollection<T> {
    T get(int index);

    int indexOf(Object o);

    int lastIndexOf(Object o);

    ReadOnlyListIterator<T> listIterator();

    ReadOnlyListIterator<T> listIterator(int index);

    List<T> subList(int fromIndex, int toIndex);

    void forEach(Consumer<? super T> action);
}
