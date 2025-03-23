using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System;

// Represents the model for the Customers page
public class CustomersModel : PageModel
{
    // List to store customer data retrieved from the database
    public List<Customer> Customers { get; set; } = new List<Customer>();

    // Method that executes when the page is accessed
    public void OnGet()
    {
        // Connection string to connect to the Northwind database
        string connectionString = "Server=localhost;Database=Northwind;User Id=sa;Password=P@ssw0rd;TrustServerCertificate=True;";

        try
        {
            // Establish a connection to the database
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open(); // Open the database connection
                
                // SQL query to retrieve customer information
                string sql = "SELECT CustomerID, CompanyName, ContactName, Country FROM Customers";

                using (var command = new SqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader()) // Execute the query and get the results
                    {
                        // Iterate through the results and populate the Customers list
                        while (reader.Read())
                        {
                            Customers.Add(new Customer
                            {
                                CustomerID = reader.GetString(0),  // Retrieve CustomerID
                                CompanyName = reader.GetString(1), // Retrieve CompanyName
                                ContactName = reader.GetString(2), // Retrieve ContactName
                                Country = reader.GetString(3)      // Retrieve Country
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any database connection or query execution errors
            Console.Error.WriteLine($"Database error: {ex.Message}");
        }
    }
}

// Class representing a Customer entity
public class Customer
{
    // Customer properties with default values to prevent null issues
    public string CustomerID { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    // Alternative: Required properties (for C# 11+)
    // public required string CustomerID { get; set; }
    // public required string CompanyName { get; set; }
    // public required string ContactName { get; set; }
    // public required string Country { get; set; }
}
