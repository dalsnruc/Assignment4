using Microsoft.EntityFrameworkCore;


namespace DataLayer
{
    public class DataService : IDataService
    {
        //Category
        public Category CreateCategory(string name, string description)
        {
            var db = new NorthwindContext();
            int id = db.Categories.Max(x => x.Id) + 1;

            var category = new Category
            {
                Id = id,
                Name = name,
                Description = description
            };

            db.Categories.Add(category);
            db.SaveChanges();
            return category;
        }

        public bool UpdateCategory(int id, string name, string description)
        {
            var db = new NorthwindContext();
            var category = db.Categories.Find(id);

            if(category == null)
            {
                return false;
            }

            category.Name = name;
            category.Description = description;

            return db.SaveChanges() > 0;

        }

        public bool UpdateCategory(Category category)
        {
            var db = new NorthwindContext();

            db.Update(category);

            return db.SaveChanges() > 0;
            
        }

        public bool DeleteCategory(int id)
        {
            var db = new NorthwindContext();

            var category = db.Categories.Find(id);

            if (category == null)
            {
                return false;
            }

            db.Categories.Remove(category);

            return db.SaveChanges() > 0;
        }

        public IList<Category> GetCategories(int page, int pageSize)
        {
            var db = new NorthwindContext();
            return db.Categories
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IList<Category> GetCategories()
        {
            var db = new NorthwindContext();
            return db.Categories.ToList();
        }

        public int NumberOfCategories()
        {
            var db = new NorthwindContext();
            return db.Categories.Count();
        }

        public Category GetCategory(int id)
        {
            var db = new NorthwindContext();
            return db.Categories.Find(id);
        }

        //Product
        public IList<Product> GetProducts()
        {
            var db = new NorthwindContext();
            return db.Products.Include(x => x.Category).ToList();
        }

        public Product GetProduct(int id)
        {
        using (var db = new NorthwindContext())
            {
                return db.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
            }
        }

        public IList<Product> GetProductByCategory(int id)
        {
            var db = new NorthwindContext();
            return db.Products.Include(x => x.Category).Where(x => x.CategoryId == id).ToList();

        }

        public IList<Product> GetProductByName(string substring)
        {
            var db = new NorthwindContext();
            return db.Products
                .Include(x => x.Category)
                .Where(x => x.ProductName
                .Contains(substring.ToLower()))
                .ToList();
        }

        //Order
        public Order GetOrder(int id)
        {
            using (var db = new NorthwindContext())
            {
                return db.Orders
                         .Include(o => o.OrderDetails) 
                         .ThenInclude(od => od.Product)
                         .ThenInclude(p => p.Category)  
                         .FirstOrDefault(o => o.Id == id); 
            }
        }

        public IList<Order> GetOrderByName(string name)
        {

            using (var db = new NorthwindContext())
            {
                return db.Orders
                             .Select(order => new Order
                             {
                                 Id = order.Id,
                                 Date = order.Date,
                                 ShippedDate = order.ShippedDate,
                                 ShipName = order.ShipName ?? "Unknown Ship Name",
                                 ShipCity = order.ShipCity ?? "Unknown City",
                             }).Where(order => order.ShipName == name)
                             .ToList();
            }
        }
        public IList<Order> GetOrders()
        {
            using (var db = new NorthwindContext())
            {
                return db.Orders
                         .Select(order => new Order
                         {
                             Id = order.Id, 
                             Date = order.Date,
                             ShippedDate = order.ShippedDate,
                             ShipName = order.ShipName ?? "Unknown Ship Name",
                             ShipCity = order.ShipCity ?? "Unknown City",
                         })
                         .ToList();
            }
        }

        //OrderDetails
        public IList<OrderDetails> GetOrderDetails()
        {
            var db = new NorthwindContext();
            return db.OrderDetails.ToList();
        }

        public IList<OrderDetails> GetOrderDetailsByOrderId(int id)
        {
            using (var db = new NorthwindContext())
            {
                return db.OrderDetails
                    .Where(od => od.OrderId == id)
                    .Include(x => x.Product)
                    .ToList();

            }

        }

        public IList<OrderDetails> GetOrderDetailsByProductId(int id)
        {
            using (var db = new NorthwindContext())
            {
                return db.OrderDetails
                         .Include(od => od.Product)
                         .Include(od => od.Order)
                         .Where(od => od.ProductId == id)
                         .OrderBy(od => od.OrderId)
                         .ToList();
            }
        }

    }

}