namespace BFF.Web.ViewModel.Catalog.Product;

 
public record CreateProductVM(string productName,
    int productCount,
    PriceVM productPrice,
    string categoryProductId);