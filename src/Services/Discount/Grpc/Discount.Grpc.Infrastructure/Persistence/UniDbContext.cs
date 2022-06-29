using Discount.Grpc.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Infrastructure.Persistence;

public class UniDbContext : DbContext
{
    public UniDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Coupon> Coupons { get; set; }
}