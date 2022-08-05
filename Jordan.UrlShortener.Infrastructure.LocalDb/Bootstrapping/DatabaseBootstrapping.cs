using Jordan.UrlShortener.Infrastructure.LocalDb.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Jordan.UrlShortener.Infrastructure.LocalDb.Bootstrapping
{
    internal static class DatabaseBootstrapping
    {
        private static string AssemblyName =>
            typeof(DatabaseBootstrapping).Assembly.FullName;

        public static IServiceCollection BootstrapDatabase(
            this IServiceCollection services,
            string connectionString
        )
        {
            void BuildDatabaseOptions(DbContextOptionsBuilder options)
            {
                options.UseSqlServer(connectionString, BuildSqlServerOptions);
            }

            void BuildSqlServerOptions(SqlServerDbContextOptionsBuilder obj)
            {
                obj.MigrationsAssembly(AssemblyName);
            }

            return services.AddDbContext<LocalDbContext>(BuildDatabaseOptions);
        }

    }
}