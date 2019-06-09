using ClassifiedAds.DomainServices;
using ClassifiedAds.DomainServices.Repositories;
using ClassifiedAds.Persistence;
using ClassifiedAds.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PersistanceServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string serviceEndPoint, string authKeyOrResourceToken, string databaseName)
        {
            services.AddDbContext<AdsDbContext>(options => options.UseCosmos(serviceEndPoint, authKeyOrResourceToken, databaseName))
                    .AddScoped<IUnitOfWork, UnitOfWork>()
                    .AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }

        public static void MigrateDb(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<AdsDbContext>().Database.EnsureCreated();
            }
        }
    }
}
