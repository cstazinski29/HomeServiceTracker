﻿using Duende.IdentityServer.EntityFramework.Options;
using HomeServiceTracker.Server.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HomeServiceTracker.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<HomeInfo> HomeInfo { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }
        public DbSet<ScheduledService> ScheduledServices { get; set; }
        public DbSet<ServiceProviderInfo> ServiceProviders { get; set; }
    }
}