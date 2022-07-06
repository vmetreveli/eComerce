// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer.Data;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer;

public class SeedData
{
    public async Task EnsureSeedData(string connectionString, string clientUrl)
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddDbContext<ApplicationDbContext>(
            options => options.UseNpgsql(connectionString)
        );

        services
            .AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddOperationalDbContext(
            options =>
            {
                options.ConfigureDbContext = db =>
                    db.UseNpgsql(
                        connectionString,
                        sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName)
                    );
            }
        );

        services.AddConfigurationDbContext(
            options =>
            {
                options.ConfigureDbContext = db =>
                    db.UseNpgsql(
                        connectionString,
                        sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName)
                    );
            }
        );

        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

        var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
        context.Database.Migrate();

        await EnsureSeedData(context, clientUrl);

        var ctx = scope.ServiceProvider.GetService<ApplicationDbContext>();
        ctx.Database.Migrate();
        EnsureUsers(scope);
    }

    private static void EnsureUsers(IServiceScope scope)
    {
        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        var identityUser = userMgr.FindByNameAsync("admin").Result;

        if (identityUser == null)
        {
            identityUser = new IdentityUser
            {
                UserName = "admin",
                Email = "admin.freeman@email.com",
                EmailConfirmed = true
            };

            var result = userMgr.CreateAsync(identityUser, "Admin123!").Result;
            if (!result.Succeeded) throw new Exception(result.Errors.First().Description);

            result =
                userMgr.AddClaimsAsync(
                    identityUser,
                    new[]
                    {
                        new(JwtClaimTypes.Name, "Angella Freeman"),
                        new Claim(JwtClaimTypes.GivenName, "Angella"),
                        new Claim(JwtClaimTypes.FamilyName, "Freeman"),
                        //  new Claim(JwtClaimTypes.Role, "admin"),
                        new Claim(JwtClaimTypes.WebSite, "http://angellafreeman.com"),
                        new Claim("location", "somewhere")
                    }
                ).Result;

            if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
        }
    }

    public async Task EnsureSeedData(ConfigurationDbContext context, string clientUrl)
    {
        if (!context.Clients.Any())
        {
            foreach (var client in Config.GetClients(clientUrl))
                await context.Clients.AddAsync(client.ToEntity());

            await context.SaveChangesAsync();
        }
        else
        {
            var oldRedirects = ( await context.Clients.Include(c => c.RedirectUris)
                    .ToListAsync() )
                .SelectMany(c => c.RedirectUris)
                .Where(ru => ru.RedirectUri.EndsWith("/o2c.html"))
                .ToList();

            if (oldRedirects.Any())
            {
                foreach (var redirectUri in oldRedirects)
                {
                    redirectUri.RedirectUri = redirectUri.RedirectUri.Replace("/o2c.html", "/oauth2-redirect.html");
                    context.Update(redirectUri.Client);
                }

                await context.SaveChangesAsync();
            }
        }

        if (!context.IdentityResources.Any())
        {
            foreach (var resource in Config.GetIdentityResources())
                await context.IdentityResources.AddAsync(resource.ToEntity());

            await context.SaveChangesAsync();
        }

        if (!context.ApiResources.Any())
        {
            foreach (var api in Config.GetApiResources())
                await context.ApiResources.AddAsync(api.ToEntity());

            await context.SaveChangesAsync();
        }

        if (!context.ApiScopes.Any())
        {
            foreach (var scope in Config.GetApiScopes())
                await context.ApiScopes.AddAsync(scope.ToEntity());

            await context.SaveChangesAsync();
        }
    }
}