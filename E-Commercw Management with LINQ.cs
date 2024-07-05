using System;
using System.Collections.Generic;
using System.Linq;

public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class OrderItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class Order
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItem> Items { get; set; }
    public string Status { get; set; } // e.g., "Pending", "Shipped", "Delivered"
}

public class InventoryItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
}

public static class Data
{
    public static List<Customer> Customers = new List<Customer>
    {
        new Customer { CustomerId = 1, Name = "Alice", Email = "alice@example.com" },
        new Customer { CustomerId = 2, Name = "Bob", Email = "bob@example.com" },
        // Add more customers as needed
    };

    public static List<InventoryItem> Inventory = new List<InventoryItem>
    {
        new InventoryItem { ProductId = 1, ProductName = "Laptop", Quantity = 10 },
        new InventoryItem { ProductId = 2, ProductName = "Smartphone", Quantity = 20 },
        // Add more inventory items as needed
    };

    public static List<Order> Orders = new List<Order>
    {
        new Order
        {
            OrderId = 1,
            CustomerId = 1,
            OrderDate = DateTime.Now.AddDays(-2),
            Items = new List<OrderItem>
            {
                new OrderItem { ProductId = 1, ProductName = "Laptop", Quantity = 1, Price = 999.99M }
            },
            Status = "Pending"
        },
        new Order
        {
            OrderId = 2,
            CustomerId = 2,
            OrderDate = DateTime.Now.AddDays(-1),
            Items = new List<OrderItem>
            {
                new OrderItem { ProductId = 2, ProductName = "Smartphone", Quantity = 2, Price = 499.99M }
            },
            Status = "Shipped"
        },
        // Add more orders as needed
    };
}

public class ECommerceSystem
{
    public void ProcessOrder(Order order)
    {
        var inventory = Data.Inventory;
        bool isAvailable = true;

        foreach (var item in order.Items)
        {
            var inventoryItem = inventory.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (inventoryItem == null || inventoryItem.Quantity < item.Quantity)
            {
                isAvailable = false;
                break;
            }
        }

        if (isAvailable)
        {
            foreach (var item in order.Items)
            {
                var inventoryItem = inventory.First(i => i.ProductId == item.ProductId);
                inventoryItem.Quantity -= item.Quantity;
            }
            order.Status = "Shipped";
            Console.WriteLine($"Order {order.OrderId} has been shipped.");
        }
        else
        {
            Console.WriteLine($"Order {order.OrderId} cannot be processed due to insufficient inventory.");
        }
    }

    public void UpdateOrderStatus(int orderId, string newStatus)
    {
        var order = Data.Orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order != null)
        {
            order.Status = newStatus;
            Console.WriteLine($"Order {order.OrderId} status has been updated to {newStatus}.");
        }
        else
        {
            Console.WriteLine($"Order {orderId} not found.");
        }
    }

    public void GenerateSalesReport(DateTime startDate, DateTime endDate)
    {
        var orders = Data.Orders
            .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
            .Select(o => new
            {
                o.OrderId,
                o.OrderDate,
                Total = o.Items.Sum(i => i.Quantity * i.Price)
            });

        Console.WriteLine($"Sales Report from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}:");
        foreach (var order in orders)
        {
            Console.WriteLine($"Order ID: {order.OrderId}, Date: {order.OrderDate.ToShortDateString()}, Total: ${order.Total}");
        }
    }

    public void FilterOrdersByStatus(string status)
    {
        var orders = Data.Orders
            .Where(o => o.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

        Console.WriteLine($"Orders with status '{status}':");
        foreach (var order in orders)
        {
            Console.WriteLine($"Order ID: {order.OrderId}, Customer ID: {order.CustomerId}, Date: {order.OrderDate.ToShortDateString()}, Status: {order.Status}");
        }
    }

    public void ViewCustomerDetails(int customerId)
    {
        var customer = Data.Customers.FirstOrDefault(c => c.CustomerId == customerId);
        if (customer != null)
        {
            Console.WriteLine($"Customer ID: {customer.CustomerId}, Name: {customer.Name}, Email: {customer.Email}");
        }
        else
        {
            Console.WriteLine($"Customer {customerId} not found.");
        }
    }

    public void AddOrder(Order newOrder)
    {
        Data.Orders.Add(newOrder);
        Console.WriteLine($"Order {newOrder.OrderId} has been added.");
    }

    public void ViewInventory()
    {
        Console.WriteLine("Current Inventory:");
        foreach (var item in Data.Inventory)
        {
            Console.WriteLine($"Product ID: {item.ProductId}, Name: {item.ProductName}, Quantity: {item.Quantity}");
        }
    }

    public void RemoveOrder(int orderId)
    {
        var order = Data.Orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order != null)
        {
            Data.Orders.Remove(order);
            Console.WriteLine($"Order {order.OrderId} has been removed.");
        }
        else
        {
            Console.WriteLine($"Order {orderId} not found.");
        }
    }
}

public class Program
{
    public static void Main()
    {
        ECommerceSystem ecommerceSystem = new ECommerceSystem();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nE-Commerce Order Processing System");
            Console.WriteLine("1. Process an Order");
            Console.WriteLine("2. Update Order Status");
            Console.WriteLine("3. Generate Sales Report");
            Console.WriteLine("4. Filter Orders by Status");
            Console.WriteLine("5. View Customer Details");
            Console.WriteLine("6. Add a New Order");
            Console.WriteLine("7. View Inventory");
            Console.WriteLine("8. Remove an Order");
            Console.WriteLine("9. Exit");
            Console.Write("Select an option (1-9): ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Enter Order ID to process: ");
                    int processOrderId = int.Parse(Console.ReadLine());
                    var orderToProcess = Data.Orders.FirstOrDefault(o => o.OrderId == processOrderId);
                    if (orderToProcess != null)
                    {
                        ecommerceSystem.ProcessOrder(orderToProcess);
                    }
                    else
                    {
                        Console.WriteLine("Order not found.");
                    }
                    break;

                case "2":
                    Console.Write("Enter Order ID to update: ");
                    int updateOrderId = int.Parse(Console.ReadLine());
                    Console.Write("Enter new status: ");
                    string newStatus = Console.ReadLine();
                    ecommerceSystem.UpdateOrderStatus(updateOrderId, newStatus);
                    break;

                case "3":
                    Console.Write("Enter start date (yyyy-MM-dd): ");
                    DateTime startDate = DateTime.Parse(Console.ReadLine());
                    Console.Write("Enter end date (yyyy-MM-dd): ");
                    DateTime endDate = DateTime.Parse(Console.ReadLine());
                    ecommerceSystem.GenerateSalesReport(startDate, endDate);
                    break;

                case "4":
                    Console.Write("Enter status to filter by: ");
                    string status = Console.ReadLine();
                    ecommerceSystem.FilterOrdersByStatus(status);
                    break;

                case "5":
                    Console.Write("Enter Customer ID to view details: ");
                    int customerId = int.Parse(Console.ReadLine());
                    ecommerceSystem.ViewCustomerDetails(customerId);
                    break;

                case "6":
                    Console.Write("Enter Order ID: ");
                    int newOrderId = int.Parse(Console.ReadLine());
                    Console.Write("Enter Customer ID: ");
                    int newOrderCustomerId = int.Parse(Console.ReadLine());
                    Console.Write("Enter number of items: ");
                    int itemCount = int.Parse(Console.ReadLine());
                    var newOrderItems = new List<OrderItem>();
                    for (int i = 0; i < itemCount; i++)
                    {
                        Console.Write("Enter Product ID: ");
                        int productId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Product Name: ");
                        string productName = Console.ReadLine();
                        Console.Write("Enter Quantity: ");
                        int quantity = int.Parse(Console.ReadLine());
                        Console.Write("Enter Price: ");
                        decimal price = decimal.Parse(Console.ReadLine());
                        newOrderItems.Add(new OrderItem { ProductId = productId, ProductName = productName, Quantity = quantity, Price = price });
                    }
                    var newOrder = new Order
                    {
                        OrderId = newOrderId,
                        CustomerId = newOrderCustomerId,
                        OrderDate = DateTime.Now,
                        Items = newOrderItems,
                        Status = "Pending"
                    };
                    ecommerceSystem.AddOrder(newOrder);
                    break;

                case "7":
                    ecommerceSystem.ViewInventory();
                    break;

                case "8":
                    Console.Write("Enter Order ID to remove: ");
                    int removeOrderId = int.Parse(Console.ReadLine());
                    ecommerceSystem.RemoveOrder(removeOrderId);
                    break;

                case "9":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid option. Please select a number between 1 and 9.");
                    break;
            }
        }
    }
}
