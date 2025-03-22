using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System;

public class CustomersModel : PageModel
{
    public List<Customer> Customers { get; set; } = new List<Customer>();

    public void OnGet()
    {
        string connectionString = "Server=localhost;Database=Northwind;User Id=sa;Password=P@ssw0rd;TrustServerCertificate=True;";

        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT CustomerID, CompanyName, ContactName, Country FROM Customers";

                using (var command = new SqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customers.Add(new Customer
                            {
                                CustomerID = reader.GetString(0),
                                CompanyName = reader.GetString(1),
                                ContactName = reader.GetString(2),
                                Country = reader.GetString(3)
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log error properly
            Console.Error.WriteLine($"Database error: {ex.Message}");
        }
    }
}

public class Customer
{
    // Option 1: Use default values to prevent null warnings
    public string CustomerID { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    // Option 2: Use `required` modifier (only in C# 11+)
    // public required string CustomerID { get; set; }
    // public required string CompanyName { get; set; }
    // public required string ContactName { get; set; }
    // public required string Country { get; set; }
}
