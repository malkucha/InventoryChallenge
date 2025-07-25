using DataAccess.Behaviours;
using DataAccess.Context;
using DataAccess.Repository;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class Extensions
    {
        public static IServiceCollection AddAplicationCoreDataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<InventoryDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddRepository<IProductRepository, ProductRepository, ProductEntity>();
            return services;
        }

        private static IServiceCollection AddRepository<ITRepository, TRepository, TEntity>(this IServiceCollection services)
            where ITRepository : class, IRepository<TEntity>
            where TRepository : class, ITRepository
            where TEntity : class
        {

            services.AddScoped<ITRepository, TRepository>();
            services.AddScoped<IRepository<TEntity>>((sp) => sp.GetRequiredService<ITRepository>());

            return services;
        }
    }
}
