using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using searchresults_api.Configuration;
using searchresults_api.Contracts;
using searchresults_api.Factory;
using searchresults_api.Services;

namespace searchresults_api
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
            // Register the Swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            // Setup Google Api configuration
            services.Configure<GoogleApiSettings>(options =>
            {
                options.ApiKey = Configuration.GetSection("GoogleApi:ApiKey").Value;
                options.EngineId = Configuration.GetSection("GoogleApi:EngineId").Value;
            });

            // Setup Ebay Api configuration
            services.Configure<EbayApiConfiguration>(options =>
            {
                options.BaseUrl = Configuration.GetSection("EbayApi:BaseUrl").Value;
                options.AppId = Configuration.GetSection("EbayApi:AppId").Value;
            });

            //Setup Yahoo configuration
            services.Configure<YahooConfiguration>(options =>
            {
                options.SearchUri = Configuration.GetSection("Yahoo:SearchUri").Value;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddTransient<ISearchService, SearchService>();

            services.AddTransient<IRestClientFactory, RestClientFactory>();
            services.AddTransient<IYahooRequestFactory, YahooRequestFactory>();
            services.AddTransient<IGoogleApiFactory, GoogleApiFactory>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
        }
    }
}
