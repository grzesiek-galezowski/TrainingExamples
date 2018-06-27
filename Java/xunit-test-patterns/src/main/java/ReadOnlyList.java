import java.util.List;
import java.util.ListIterator;
import java.util.function.Consumer;

public class ReadOnlyList<T> extends ReadOnlyCollection<T> {
    private final List<T> original;

    public ReadOnlyList(List<T> list) {
        super(list);
        original = list;
    }

    public T get(int index) {
        return original.get(index);
    }

    public int indexOf(Object o) {
        return original.indexOf(o);
    }

    public int lastIndexOf(Object o) {
        return original.lastIndexOf(o);
    }

    public ReadOnlyListIterator<T> listIterator() {
        return new ReadOnlyListIterator<>(original.listIterator());
    }

    public ListIterator<T> listIterator(int index) {
        return original.listIterator(index);
    }

    public List<T> subList(int fromIndex, int toIndex) {
        return original.subList(fromIndex, toIndex);
    }

    public void forEach(Consumer<? super T> action) {
        original.forEach(action);
    }
}
