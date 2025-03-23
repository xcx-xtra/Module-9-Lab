using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Northwind.Pages; // Defines the namespace for the Razor Page

public class IndexModel : PageModel // Defines a Razor Page model for the Index page
{
    private readonly ILogger<IndexModel> _logger; // Logger instance for logging messages

    // Constructor to initialize the logger
    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    // Handles GET requests when the Index page is loaded
    public void OnGet()
    {
        // Currently, no logic is executed when the page loads
    }
}
