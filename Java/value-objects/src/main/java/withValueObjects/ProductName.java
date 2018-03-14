package withValueObjects;

import org.apache.commons.lang3.StringUtils;

public final class ProductName {
    private static final ProductName productName = new ProductName(""); //what is this?
    private final String capitalizedName;

    static ProductName empty() {
        return productName;
    }

    public static ProductName from(String name) {
        if(name == null) {
            throw new IllegalArgumentException("name cannot be null");
        }
        if("".equals(name)) {
            throw new IllegalArgumentException("name cannot be empty");
        }
        return new ProductName(name);
    }

    private ProductName(String name) {
        this.capitalizedName = StringUtils.capitalize(name);
    }

    @Override
    public String toString() {
        return capitalizedName;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) {
            return true;
        }
        if (o == null || getClass() != o.getClass()) {
            return false;
        }

        ProductName that = (ProductName) o;

        return capitalizedName != null ? capitalizedName.equals(that.capitalizedName) : that.capitalizedName == null;
    }

    @Override
    public int hashCode() {
        return capitalizedName != null ? capitalizedName.hashCode() : 0;
    }
}
