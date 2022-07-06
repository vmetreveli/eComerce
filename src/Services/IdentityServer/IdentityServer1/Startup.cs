// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer.Data;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer;

public class Startup
{
    public Startup(IWebHostEnvironment environment, IConfiguration configuration)
    {
        Environment = environment;
        Configuration = configuration;
    }

    public IWebHostEnvironment Environment { get; }
    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();

        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        var assembly = typeof(Program).Assembly.GetName().Name;

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString, b => b.MigrationsAssembly(assembly)));

        services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                // config.Password.RequireDigit = false;
                // config.Password.RequireLowercase = false;
                // config.Password.RequireNonAlphanumeric = false;
                // config.Password.RequireUppercase = false;
                // config.Password.RequiredLength = 5;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.ConfigureApplicationCookie(config =>
        {
            //config.LoginPath = "/Auth/Login";
            //config.LogoutPath = "/Auth/Logout";
            config.Cookie.Name = "IdentityServer.Cookies";
        });

        services.AddIdentityServer(options =>
            {
                // options.Events.RaiseErrorEvents = true;
                // options.Events.RaiseInformationEvents = true;
                // options.Events.RaiseFailureEvents = true;
                // options.Events.RaiseSuccessEvents = true;
                //
                // // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                // options.EmitStaticAudienceClaim = true;
            })
            .AddAspNetIdentity<IdentityUser>()
            .AddTestUsers(TestUsers.Users)
            // this adds the config data from DB (clients, resources, CORS)
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString, opt =>
                    opt.MigrationsAssembly(assembly));
            })
            // this adds the operational data from DB (codes, tokens, consents)
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString, opt =>
                    opt.MigrationsAssembly(assembly));

                // this enables automatic token cleanup. this is optional.
                options.EnableTokenCleanup = true;
            })
            // not recommended for production - you need to store your key material somewhere secure
            .AddDeveloperSigningCredential();

        services.AddAuthentication();
    }

    public void Configure(IApplicationBuilder app)
    {
        if (Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseIdentityServer();

        app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
    }
    /*
     dotnet  ef migrations add "InitialAspNetIdentityMigration"  --context   PersistedGrantDbContext
     dotnet  ef database  update --context PersistedGrantDbContext

     dotnet  ef migrations add "InitialAspNetIdentityMigration"  --context   ConfigurationDbContext
     dotnet  ef database  update --context ConfigurationDbContext

     dotnet  ef migrations add "InitialAspNetIdentityMigration"  --context   ApplicationDbContext
     dotnet  ef database  update --context ApplicationDbContext
     */

    /*
     * First Run From Terminal
     * dotnet run /seed --project IdentityServer
     */
}