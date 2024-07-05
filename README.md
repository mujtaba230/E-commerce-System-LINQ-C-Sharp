
 #E-Commerce Order Processing System

This console application is designed to manage e-commerce orders, customer details, inventory, and order processing operations. It allows users to perform various actions related to orders and inventory through a menu-driven interface.

## Languages Used
- C# with LINQ

 Features

1. **Process an Order**: Check inventory availability and update order status to "Shipped" if items are available.
   
2. **Update Order Status**: Change the status of an existing order (e.g., from "Pending" to "Delivered").

3. **Generate Sales Report**: Calculate total sales within a specified date range based on order items.

4. **Filter Orders by Status**: View orders based on their current status (e.g., "Shipped", "Pending").

5. **View Customer Details**: Display details of a specific customer based on their ID.

6. **Add a New Order**: Add a new order with customer ID, order items, and status.

7. **View Inventory**: Display current inventory items and their quantities.

8. **Remove an Order**: Delete an existing order from the system.

 Getting Started

To run the application:

1. Clone or download the repository to your local machine.
   
2. Open the solution in Visual Studio or any C# IDE that supports .NET development.

3. Build the solution to restore packages and compile the code.

4. Run the application (`ECommerceSystem.ConsoleApp`) from the IDE.

5. Follow the on-screen prompts to perform various actions on orders and inventory.

 Usage

- Upon running the application, a menu will be displayed with options to perform actions like processing orders, updating statuses, generating reports, etc.

- Enter the corresponding number for each action you wish to perform and follow the prompts for necessary inputs (e.g., order IDs, dates, customer IDs, etc.).

- The application will output results or notifications based on the actions performed (e.g., success messages, error messages, confirmation of actions).

 Notes

- This application uses sample data for customers, orders, and inventory items defined within the `Data` class. You can modify or extend these data structures to suit your needs.

- Error handling is included for basic scenarios (e.g., order not found, insufficient inventory).

- Feel free to explore and extend the functionality based on your requirements, such as adding more complex business rules or integrating with external systems.
