using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Atento.FileManager.Web.Api.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Atento.FileManager.Services;


namespace Atento.FileManager.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            
            var config = new ServerConfig();
            Configuration.Bind(config);           

            var todoContext = new TodoContext(config.MongoDB);
            var repo = new TodoRepository(todoContext);
            services.AddSingleton<ITodoRepository>(repo);

            var fileContext = new StoredFileContext(config.MongoDB);
            var fileRepo = new StoredFileRepository(fileContext);
            var fileService = new FileStorageService(fileRepo);

            
            services.AddSingleton<IStoredFileRepository>(fileRepo);
            services.AddSingleton<IFileStorageService>(fileService);
            

            services.AddControllers();

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
