using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementApp;
using ProcessApp;

class Program
{
    static void Main()
    {
        // Налаштовуємо InMemory DB
        var options = new DbContextOptionsBuilder<RestaurantContext>()
            .UseInMemoryDatabase("RestaurantDB")
            .Options;

        using var context = new RestaurantContext(options);

        // --- Створюємо клієнтів ---
        var customers = new[]
        {
            new Customer { Name="Sofia", PhoneNumber="123", Address="Kyiv", Profile=new CustomerProfile { Preferences="Vegan" } },
            new Customer { Name="Olena", PhoneNumber="456", Address="Lviv", Profile=new CustomerProfile { Preferences="Gluten-Free" } },
            new Customer { Name="Ivan", PhoneNumber="789", Address="Odessa", Profile=new CustomerProfile { Preferences="Spicy" } }
        };
        context.Customers.AddRange(customers);

        // --- Створюємо продукти ---
        var products = new[]
        {
            new Product { Name="Pizza", Price=10 },
            new Product { Name="Burger", Price=7 },
            new Product { Name="Salad", Price=5 },
            new Product { Name="Soup", Price=4 },
            new Product { Name="Pasta", Price=8 }
        };
        context.Products.AddRange(products);

        // --- Створюємо замовлення ---
        var orders = new[]
        {
            new Order { Name="Order #1", Customer=customers[0] },
            new Order { Name="Order #2", Customer=customers[1] },
            new Order { Name="Order #3", Customer=customers[2] },
        };
        context.Orders.AddRange(orders);

        // --- Прив'язуємо продукти до замовлень (Many-to-Many) ---
        var orderProducts = new[]
        {
            new OrderProduct { Order=orders[0], Product=products[0] },
            new OrderProduct { Order=orders[0], Product=products[2] },
            new OrderProduct { Order=orders[1], Product=products[1] },
            new OrderProduct { Order=orders[1], Product=products[3] },
            new OrderProduct { Order=orders[2], Product=products[4] },
        };
        context.OrderProducts.AddRange(orderProducts);

        context.SaveChanges();

        // --- Виводимо замовлення ---
        foreach (var order in context.Orders.Include(o => o.Customer).Include(o => o.OrderProducts).ThenInclude(op => op.Product))
        {
            Console.WriteLine($"Order: {order.Name}, Customer: {order.Customer.Name}");
            foreach (var op in order.OrderProducts)
            {
                Console.WriteLine($"  Product: {op.Product.Name}, Price: {op.Product.Price}");
            }

            // --- Створюємо і запускаємо процес ---
            var process = new Process { Id=order.Id, Name=order.Name };
            process.Start();
            process.Complete();
            Console.WriteLine($"Current status: {process.Status}");
            Console.WriteLine();
        }
    }
}
