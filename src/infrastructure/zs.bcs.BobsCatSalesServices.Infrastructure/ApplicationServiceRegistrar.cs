﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using zs.bcs.BobsCatSalesServices.Application.Entity.Commands;
using zs.bcs.BobsCatSalesServices.Application.Interfaces.Persistence.SalesAssociate;
using zs.bcs.BobsCatSalesServices.Application.Interfaces.Services;
using zs.bcs.BobsCatSalesServices.Events;
using zs.bcs.BobsCatSalesServices.Infrastructure.Services;
using zs.bcs.BobsCatSalesServices.Persistence.MsSql;
using zs.bcs.BobsCatSalesServices.Persistence.MsSql.Services.SalesAssociate;

namespace zs.bcs.BobsCatSalesServices.Infrastructure
{
    /// <summary>
    /// Registers dependent services for the application.
    /// </summary>
    public static class ApplicationServiceRegistrar
    {
        /// <summary>
        /// Register services in the service pipeline.
        /// </summary>
        /// <param name="services">The service pipeline.</param>
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            services.AddTransient<IPasswordSecurityService, PasswordSecurityService>();
            services.AddTransient<IEntityKeyGenerator, EntityKeyGenerator>();

            services.AddScoped(typeof(IRequestHandler<,>), typeof(Application.Entity.Handlers.InitializeEntityHandler<,>));

            services.AddScoped(typeof(IBobsCatSalesEventService<,>), typeof(BobsCatSalesEventService<,>));

            services.AddDbContext<BobsCatSalesDbContext>();
            services.AddScoped<ISalesAssociatePersistenceQueries, SalesAssociatePersistenceQueries>();
            services.AddScoped<ISalesAssociatePersistenceCommands, SalesAssociatePersistenceCommands>();

            return services;
        }
    }
}
