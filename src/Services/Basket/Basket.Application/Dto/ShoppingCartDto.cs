using System.Collections.Generic;
using System.Linq;

namespace Basket.Application.Dto;

public class ShoppingCartDto
{
    public ShoppingCartDto()
    {
    }

    public ShoppingCartDto(string userName) => UserName = userName;

    public string UserName { get; set; }
    public List<ShoppingCartItemDto> Items { get; set; } = new();

    public decimal TotalPrice =>
        Items.Sum(item => item.Price * item.Quantity);
}