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
using System.Collections.Generic;
using IdentityServer4.AccessTokenValidation;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

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
            services.AddScoped<EmployeeService>();
            services.AddScoped<OrganizationService>();

            services.Configure<BlobConection>(Configuration.GetSection("BlobConnections"));
            services.AddSingleton<BlobConection>();


            services.AddScoped<BlobStorageService>();
          
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

            services.Configure<BlobConection>(Configuration.GetSection("BlobConnections"));
            services.AddSingleton<BlobConection>();

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


            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
    
                            .AddIdentityServerAuthentication(options =>
                            {
                                var settings = new IdentityServerSettings();

                                Configuration.Bind("IdentityServerSettings", settings);

                                options.Authority = settings.Authority;
                                options.RequireHttpsMetadata = settings.RequireHttpsMetadata;
                                options.ApiName = settings.ApiName;
                            });
                       
      
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "Protected API", Version = "v1" });

                var settings = new MastpenIdentityClientSettings();

                Configuration.Bind("MastpenIdentityClientSettings", settings);

                options.AddSecurityDefinition("oauth2",
                    
                 new OAuth2Scheme
                {
                    Flow = "implicit", // just get token via browser (suitable for swagger SPA)
                    AuthorizationUrl = settings.Url,// "https://malammastpenapiidentityserver.azurewebsites.net/connect/authorize",
                    Scopes = new Dictionary<string, string> { { settings.ClientSecret, settings.ClientSecret+" API - full access" } }
                });


                options.OperationFilter<AuthorizeCheckOperationFilter>(); // Required to use access token
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

            app.UseAuthentication();

            // Swagger JSON Doc
            app.UseSwagger();

            // Swagger UI
            app.UseSwaggerUI(options =>
            {

                var settings = new MastpenIdentityClientSettings();

                Configuration.Bind("MastpenIdentityClientSettings", settings);

                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                options.RoutePrefix = string.Empty;

            options.OAuthClientId(settings.ClientId);// "MastpenWebAPI_swagger");
                options.OAuthAppName(settings.ClientSecret+" API - Swagger"); // presentation purposes only
            });

            loggerFactory.AddLog4Net();

            app.UseMvc();
        }


    }
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {

            var globalAttributes = context.ApiDescription.ActionDescriptor.FilterDescriptors.Select(p => p.Filter);
            var controlerAttributes = context.MethodInfo?.DeclaringType?.GetCustomAttributes(true);
            var methodAttributes = context.MethodInfo?.GetCustomAttributes(true);
            var hasAuthorize = globalAttributes
                .Union(controlerAttributes)
                .Union(methodAttributes)
                .OfType<AuthorizeAttribute>().Any();

            if (hasAuthorize)
            {
                operation.Responses.Add("401", new Response { Description = "Unauthorized" });
                operation.Responses.Add("403", new Response { Description = "Forbidden" });

                operation.Security = new List<IDictionary<string, IEnumerable<string>>>
                {
                    new Dictionary<string, IEnumerable<string>> {{"oauth2", new[] {"MastpenWebAPI"}}}
                };
            }
        }
    }
#pragma warning restore CS1591
}

     