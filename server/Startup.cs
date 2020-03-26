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
        private const string API_TITLE = "FileManager API";
        private const string API_VERSION = "v1";
        private string API_NAME = $"{API_TITLE} - {API_VERSION}";
        private string API_SWAGGER_URL = $"/swagger/{API_VERSION}/swagger.json";

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
                c.SwaggerDoc(API_VERSION, new OpenApiInfo { Title = API_TITLE, Version = API_VERSION });
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
                c.SwaggerEndpoint(API_SWAGGER_URL, API_NAME);
            });
        }
    }
}
