using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Malam.Mastpen.API.Controllers;

using Malam.Mastpen.Core.DAL;
using Malam.Mastpen.API.Filters;
using Malam.Mastpen.API.Security;
using Malam.Mastpen.API.PolicyRequirements;
using Malam.Mastpen.API.Clients;
using Malam.Mastpen.API.Clients.Contracts;
using Malam.Mastpen.Core;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Malam.Mastpen.Core.BL.Contracts;
using Malam.Mastpen.Core.BL.Services;

namespace Malam.Mastpen.API
{
#pragma warning disable CS1591
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

            /* Configuration for MVC */
            var appSettingsSection = Configuration.GetSection("AppSettings");
                services.AddMvc(config =>
            {
                config.Filters.Add(typeof(MastpenExceptionFilter));
                config.Filters.Add(typeof(MastpenActionFilter));
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

     

            /* Setting dependency injection */
            // For DbContext
            services.AddDbContext<MastpenBitachonDbContext>(options =>
            {
                options.UseSqlServer(Configuration["AppSettings:ConnectionString"]);
            });

            // User info
            services.AddScoped<IUserInfo, UserInfo>();

           //ADD SERVICES
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
          
            //// Logger for services
            //services.AddScoped<ILogger, Logger<Service>>();

            /* Identity Server for Mastpen */
            services.Configure<MastpenIdentityClientSettings>(Configuration.GetSection("MastpenIdentityClientSettings"));
            services.AddSingleton<MastpenIdentityClientSettings>();

            services.Configure<RothschildHouseIdentitySettings>(Configuration.GetSection("RothschildHouseIdentitySettings"));
            services.AddSingleton<RothschildHouseIdentitySettings>();


            /*face recognition*/
            services.Configure<MastpenIdentityClientSettings>(Configuration.GetSection("MastpenFaceRecognitionSettings"));
            services.AddSingleton<MastpenIdentityClientSettings>();


            services.AddScoped<IRothschildHouseIdentityClient, RothschildHouseIdentityClient>();
        
            /* Mastpen Services */


            /* Configuration for authorization */

            services
                .AddMvcCore()
                .AddAuthorization(options =>
                {
                    options
                        .AddPolicy(Policies.AdministratorPolicy, builder => builder.Requirements.Add(new AdministratorPolicyRequirement()));
                    options
                        .AddPolicy(Policies.CustomerPolicy, builder => builder.Requirements.Add(new CustomerPolicyRequirement()));
                });

            /* Configuration for Identity Server authentication */

            services
                .AddAuthentication("Bearer")
                 //.AddJwtBearer(GetJwtBearerOptions)
                .AddIdentityServerAuthentication(options =>
                {
                    var settings = new IdentityServerSettings();

                    Configuration.Bind("IdentityServerSettings", settings);

                    options.Authority = settings.Authority;
                    options.RequireHttpsMetadata = settings.RequireHttpsMetadata;
                    options.ApiName = settings.ApiName;
                });

            /* Configuration for Help page */


            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Mastpen Bitachon API";
                    document.Info.Description = "מצפן ביטחון";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Malam Team",
                        Email = string.Empty,
                        Url = "https://www.malamteam.com/"
                    };
                    //document.Info.License = new NSwag.OpenApiLicense
                    //{
                    //    Name = "Use under LICX",
                    //    Url = "https://example.com/license"
                    //};
                };
            });




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors(builder =>
            {
                // Add client origin in CORS policy

                // todo: Set port number for client app from appsettings file

                builder.WithOrigins("http://localhost:4200");
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            //loggerFactory.lo
            app.UseAuthentication();
            // Enable mIddleware to serve generated Swagger as a JSON endpoint.
            app.UseOpenApi();
            app.UseSwaggerUi3();

            loggerFactory.AddLog4Net();

            app.UseMvc();
        }

        //private static void GetJwtBearerOptions(JwtBearerOptions options)
        //{
        //    options.Authority = "http://localhost:5000";
        //    options.Audience = "http://localhost:5000/resources";
        //    options.RequireHttpsMetadata = false;
        //}

    }

#pragma warning restore CS1591
}

     