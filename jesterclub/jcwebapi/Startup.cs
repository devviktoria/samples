using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace jcwebapi {
    public class Startup {
        readonly string JesterClubSpecificOrigins = "_JesterClubSpecificOrigins";

        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            services.AddCors (options => {
                options.AddPolicy (name: JesterClubSpecificOrigins,
                    builder => {
                        builder.AllowAnyOrigin ()
                            .AllowAnyMethod ()
                            .AllowAnyHeader ();
                    });
            });

            jcmongodbservice.Repository.MongoDbDatabase.RegisterMongoDbServices (services);
            services.AddControllers ().AddNewtonsoftJson (options => options.UseMemberCasing ());
            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new OpenApiInfo { Title = "jcwebapi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {

            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
                app.UseSwagger ();
                app.UseSwaggerUI (c => c.SwaggerEndpoint ("/swagger/v1/swagger.json", "jcwebapi v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting ();

            app.UseCors (JesterClubSpecificOrigins);

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}