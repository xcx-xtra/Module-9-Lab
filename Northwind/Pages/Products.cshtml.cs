// Import necessary namespaces
using Microsoft.AspNetCore.Mvc.RazorPages;  // Used for Razor Pages functionality
using System.Collections.Generic;  // Used for defining lists
using Microsoft.Data.SqlClient;  // Used for SQL database connection

// The ProductsModel class is responsible for fetching product data and displaying it
public class ProductsModel : PageModel
{
    // Property to hold the list of products that will be displayed on the page
    public List<Product> Products { get; set; } = new List<Product>();

    // This method is called when the page is accessed (GET request)
    public void OnGet()
    {
        // Connection string to connect to the local SQL Server
        string connectionString = "Server=localhost;Database=Northwind;User Id=sa;Password=P@ssw0rd;TrustServerCertificate=True;";

        try
        {
            // Create and open the SQL connection using the provided connection string
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();  // Open the connection to the database

                // SQL query to fetch product names, category names, and unit prices
                string sql = @"SELECT p.ProductName, c.CategoryName, p.UnitPrice
                               FROM Products p
                               JOIN Categories c ON p.CategoryID = c.CategoryID";

                // Create a SQL command using the query and connection
                using (var command = new SqlCommand(sql, connection))
                {
                    // Execute the query and get the data from the database
                    using (var reader = command.ExecuteReader())
                    {
                        // Loop through each row returned by the SQL query
                        while (reader.Read())
                        {
                            // Add a new Product to the Products list with the data retrieved from the database
                            Products.Add(new Product
                            {
                                ProductName = reader.GetString(0),  // Get the product name from the first column
                                CategoryName = reader.GetString(1),  // Get the category name from the second column
                                UnitPrice = reader.GetDecimal(2)  // Get the unit price from the third column
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // If an error occurs during the database operation, print the error message
            Console.Error.WriteLine($"Database error: {ex.Message}");
        }
    }
}

// Define the Product class to hold product data
public class Product
{
    public string ProductName { get; set; } = string.Empty;  // Product name (default is empty string)
    public string CategoryName { get; set; } = string.Empty;  // Category name (default is empty string)
    public decimal UnitPrice { get; set; }  // Unit price of the product
}
