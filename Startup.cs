using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Geocoding.Google;
using map_api.Data;
using map_api.Data.Business;
using map_api.Data.Json;
using map_api.Data.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace map_api
{
    /// <summary>
    /// Startup class used for configuration of APP
    /// </summary>
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }

        /// <summary>
        /// Used for necessary services registration
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //Cors configuration
            services.AddCors(options =>
            {
                options.AddPolicy(Constants.PolicyName,
                    builder => builder.WithOrigins(Configuration[Constants.AllowCorsUrls]) //Currently supports single url
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials() );
            });
            //End: Cors configuration
            


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ObjectMapper());
            });
            
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            string dataAccessProvider = Configuration[Constants.DataSourceProvider];
            
            if(dataAccessProvider == "JSON")
                services.AddScoped<IDataAccess, JsonDataAccess>();

            services.AddScoped<IMapResponseProcessor, MapResponseProcessor>();

            string apiKey = Configuration[Constants.GeoApiKey];
            services.AddSingleton<IMapService, MapService>();

            //Swagger integration: service setup
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Map REST api for Marker CRUD operations",
                    Description = "Map REST api for Marker CRUD operations",
                    TermsOfService = "None"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });
            //End: Swagger integration: service setup
        }


        /// <summary>
        /// Configures registered services
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //Cors setup
            app.UseCors(); 


            app.UseMvc();

            //Added swagger middle wire
            app.UseSwagger(); 
            
            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MAP API v1");
                });
            }
            //End: Added swagger middle wire
    }
}
