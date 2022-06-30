using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics.Pages;

public class IndexModel : PageModel
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public IndexModel(IProductRepository productRepository, ICartRepository cartRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
    }

    public IEnumerable<Product> ProductList { get; set; } = new List<Product>();

    public async Task<IActionResult> OnGetAsync()
    {
        ProductList = await _productRepository.GetProducts();
        return Page();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(int productId)
    {
        //if (!User.Identity.IsAuthenticated)
        //    return RedirectToPage("./Account/Login", new { area = "Identity" });

        await _cartRepository.AddItem("test", productId);
        return RedirectToPage("Cart");
    }
}