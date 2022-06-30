﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics;

public class CheckOutModel : PageModel
{
    private readonly ICartRepository _cartRepository;
    private readonly IOrderRepository _orderRepository;

    public CheckOutModel(ICartRepository cartRepository, IOrderRepository orderRepository)
    {
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    [BindProperty] public Order Order { get; set; }

    public Cart Cart { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        Cart = await _cartRepository.GetCartByUserName("test");
        return Page();
    }

    public async Task<IActionResult> OnPostCheckOutAsync()
    {
        Cart = await _cartRepository.GetCartByUserName("test");

        if (!ModelState.IsValid) return Page();

        Order.UserName = "test";
        Order.TotalPrice = Cart.TotalPrice;

        await _orderRepository.CheckOut(Order);
        await _cartRepository.ClearCart("test");

        return RedirectToPage("Confirmation", "OrderSubmitted");
    }
}