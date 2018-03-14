package withValueObjects;

import java.util.Optional;
import java.util.TreeMap;

@SuppressWarnings("unused")
public class Products {

    private TreeMap<ProductName, Product> productsByName = new TreeMap<>();
    private ProductName topProductName = ProductName.empty();

    public void setTopProductName(ProductName topProductName) {
        this.topProductName = topProductName;
    }

    public void add(ProductName productName, Product product) {
        productsByName.put(productName, product);
    }

    public int getQuantityOf(ProductName productName) {
        //comparing!!
        Optional<Product> maybeProduct =
            Optional.ofNullable(productsByName.get(productName));
        return maybeProduct.map(p -> p.getQuantity()).orElse(0);
    }

    public boolean contains(ProductName productName) {
        //comparing!!
        return productsByName.containsKey(productName);
    }

    public boolean isTopProduct(ProductName productName) {
        return productName.equals(topProductName);
    }


}
