using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using zs.bcs.BobsCatSalesServices.Application.Interfaces.Services;
using zs.bcs.BobsCatSalesServices.Infrastructure.Services;

namespace zs.bcs.BobsCatSalesServices.Infrastructure
{
    /// <summary>
    /// Registers dependent services for the application.
    /// </summary>
    public static class ApplicationServiceRegistrar
    {

        public static void RegisterApplicationServices(this IServiceCollection services)
        {




            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


            services.AddTransient<IPasswordSecurityService, PasswordSecurityService>();




        }


    }
}
