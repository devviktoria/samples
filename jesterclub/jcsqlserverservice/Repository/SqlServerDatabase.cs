using jcinterfaces.Service;
using jcsqlserverservice.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace jcsqlserverservice.Repository {
    public static class SqlServerDatabase {
        public static void RegisterSqlServerServices (IServiceCollection services) {
            string dbConnectionString = System.Environment.GetEnvironmentVariable ("JCSSDB");
            services.AddDbContext<SqlServerDbContext> (options =>
                options.UseSqlServer (dbConnectionString));

            services.AddScoped<IJokeService, JokeService> ();
            services.AddSingleton<IJokeConverterService, JokeConverterService> ();
        }
    }
}