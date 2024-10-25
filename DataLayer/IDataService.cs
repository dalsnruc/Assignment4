namespace DataLayer;

public interface IDataService
{
    //Category
    IList<Category> GetCategories();
    IList<Category> GetCategories(int page, int pageSize);
    int NumberOfCategories();
    Category CreateCategory(string name, string description);
    bool DeleteCategory(int id);
    bool UpdateCategory(int id, string name, string description);
    bool UpdateCategory(Category category);
    Category GetCategory(int id);


    //Product
    IList<Product> GetProducts();
    Product GetProduct(int id);

    IList<Product> GetProductByCategory(int id);

    IList<Product> GetProductByName(string substring);

    //Order
    IList<Order> GetOrders();
    Order GetOrder(int id);

    IList <Order> GetOrderByName(string name);


    //OrderDetails
    IList<OrderDetails> GetOrderDetails();
    IList <OrderDetails> GetOrderDetailsByOrderId(int id);
    IList <OrderDetails> GetOrderDetailsByProductId(int id);

}
