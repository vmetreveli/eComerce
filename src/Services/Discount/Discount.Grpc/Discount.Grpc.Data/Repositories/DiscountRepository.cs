using Dapper;
using Discount.Grpc.Domain.Interfaces.Repository;
using Discount.Grpc.Domain.Models.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Grpc.Data.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly IConfiguration _config;
    private readonly string _connectionString = "DefaultConnection";

    public DiscountRepository(IConfiguration configuration) =>
        _config = configuration ?? throw new ArgumentNullException(nameof(configuration));

    public async Task<Coupon> GetDiscount(string productName, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_config.GetConnectionString(_connectionString));
       var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(new CommandDefinition(
            "SELECT * FROM Coupon WHERE ProductName = @ProductName",
            new {ProductName = productName}, cancellationToken: cancellationToken));

        // if (coupon == null)
        //     return new Coupon
        //         {ProductName = "No Discount", Amount = 0, Description = "No Discount Desc"};

        return coupon;
    }

    public async Task<bool> CreateDiscount(Coupon coupon, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_config.GetConnectionString(_connectionString));
        var affected =
            await connection.ExecuteAsync
            (new CommandDefinition(
                "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                new {coupon.ProductName, coupon.Description, coupon.Amount}
                , cancellationToken: cancellationToken));

        return affected != 0;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_config.GetConnectionString(_connectionString));

        var affected = await connection.ExecuteAsync
        (new CommandDefinition(
            "UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
            new {coupon.ProductName, coupon.Description, coupon.Amount, coupon.Id}
            , cancellationToken: cancellationToken));

        return affected != 0;
    }

    public async Task<bool> DeleteDiscount(string productName, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_config.GetConnectionString(_connectionString));

        var affected = await connection.ExecuteAsync(new CommandDefinition(
            "DELETE FROM Coupon WHERE ProductName = @ProductName",
            new {ProductName = productName}, cancellationToken: cancellationToken));

        return affected != 0;
    }
}