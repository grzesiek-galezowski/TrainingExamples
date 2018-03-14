package withoutValueObjects;

import java.util.Optional;
import java.util.TreeMap;

@SuppressWarnings("unused")
public class Products {

    private TreeMap<String, Product> productsByName = new TreeMap<>();
    private String topProductName = "";

    public void setTopProductName(String topProductName) {
        this.topProductName = topProductName;
    }

    public void add(String productName, Product product) {
        productsByName.put(productName, product);
    }

    public int getQuantityOf(String productName) {
        //comparing!!
        Optional<Product> maybeProduct =
            Optional.ofNullable(productsByName.get(productName));
        return maybeProduct.map(p -> p.getQuantity()).orElse(0);
    }

    public boolean contains(String productName) {
        //comparing!!
        return productsByName.containsKey(productName);
    }

    public boolean isTopProduct(String productName) {
        //comparing!!
        return productName.equals(topProductName);
    }


    //option: case insensitive everywhere
    //option: toLowerCase
    //option: helper
    //option: value object
}
