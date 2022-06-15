using Discount.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.Data.Context;

public class UniDbContext : DbContext
{
    public UniDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Coupon> Coupons { get; set; }
}