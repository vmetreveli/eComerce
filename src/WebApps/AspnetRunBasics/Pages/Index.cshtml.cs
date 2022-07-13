using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
namespace AspnetRunBasics.Pages;
[Authorize]
public class IndexModel : PageModel
{
    private readonly ICatalogService _catalogService;
    private readonly IBasketService _basketService;

    public IndexModel(IBasketService basketService, ICatalogService catalogService)
    {
        _basketService = basketService;
        _catalogService = catalogService;
    }

    public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();

    public async Task<IActionResult> OnGetAsync()
    {
        await LogTokenAndClaims();
        ProductList = await _catalogService.GetCatalog();
        return Page();
    }
    public async Task LogTokenAndClaims()
    {
        var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);

        Debug.WriteLine($"Identity token: {identityToken}");

        foreach (var claim in User.Claims)
        {
            Debug.WriteLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
        }
    }
    public async Task<IActionResult> OnPostAddToCartAsync(string productId)
    {
        //if (!User.Identity.IsAuthenticated)
        //    return RedirectToPage("./Account/Login", new { area = "Identity" });
        var product = await _catalogService.GetCatalog(productId);
        const string userName = "swn";
        var basket = await _basketService.GetBasket(userName);

        basket.Items.Add(new BasketItemModel
        {
            ProductId = productId,
            ProductName = product.Name,
            Price = product.Price,
            Quantity = 1,
            Color = "Black"
        });

       var basketUpdated= await _basketService.UpdateBasket(basket);
        return RedirectToPage("Cart");
    }
}