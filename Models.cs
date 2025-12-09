using System;
using System.Collections.Generic;

namespace RestaurantManagementApp
{
    // Клієнт (one-to-one з профілем)
    public class Customer
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }

        public CustomerProfile Profile { get; set; } = null!; // one-to-one
        public List<Order> Orders { get; set; } = new(); // one-to-many
    }

    // Профіль клієнта (one-to-one)
    public class CustomerProfile
    {
        public int Id { get; set; }
        public required string ProfileInfo { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
    }

    // Замовлення (many-to-one з клієнтом)
    public class Order
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        // many-to-many з продуктами
        public List<OrderProduct> OrderProducts { get; set; } = new();
    }

    // Продукт
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }

        public List<OrderProduct> OrderProducts { get; set; } = new(); // many-to-many
    }

    // Join table для many-to-many
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}