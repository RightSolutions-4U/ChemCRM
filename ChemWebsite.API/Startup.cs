using ChemWebsite.Api.Helpers;
using ChemWebsite.API.Helpers.Mapping;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.PipeLineBehavior;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using ChemWebsite.Repository;
using Hangfire;
using Hangfire.SqlServer;
using ChemWebsite.API.Helpers;

namespace ChemWebsite.API
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
            var assembly = AppDomain.CurrentDomain.Load("ChemWebsite.MediatR");
            var defaultUserId = Configuration.GetSection("DefaultUser").GetSection("DefaultUserId").Value;
            services.AddMediatR(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssemblies(Enumerable.Repeat(assembly, 1));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            JwtSettings settings;
            settings = GetJwtSettings();
            services.AddSingleton<JwtSettings>(settings);
            services.AddSingleton<DashboardStatics>();
            services.AddTransient<JobService>();
            services.AddSingleton<PathHelper>(new PathHelper(Configuration));
            services.AddSingleton<IConnectionMappingRepository, ConnectionMappingRepository>();
            services.AddScoped<UserInfoToken>(c => new UserInfoToken() { Id = defaultUserId });
            services.AddDbContextPool<ChemWebsiteDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DbConnectionString"))
                .EnableSensitiveDataLogging();
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            services.AddIdentity<User, Role>()
             .AddEntityFrameworkStores<ChemWebsiteDbContext>()
             .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });
            services.AddSingleton(MapperConfig.GetMapperConfigs());
            services.AddDependencyInjection();
            services.AddJwtAutheticationConfiguration(settings);
            services.AddCors(options =>
            {
                options.AddPolicy("ExposeResponseHeaders",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4000",
                                            "http://localhost:4200",
                                             "http://localhost:4201",
                                             "https://usermgt.mlglobtech.com",
                                             "https://www.usermgt.mlglobtech.com"

                                         )
                               .WithExposedHeaders("X-Pagination")
                               .AllowAnyHeader()
                               .AllowCredentials()
                               .WithMethods("POST", "PUT", "PATCH", "GET", "DELETE")
                               .SetIsOriginAllowed(host => true);
                    });
            });

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("DbConnectionString"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            services.AddSignalR();
            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Chem Website API"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });

                //Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //var jobService = sp.GetService<JobService>();
            //jobService.StartScheduler();
            SpaStartup.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IWebHostEnvironment env, JobService jobService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandlerFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global exception logger");
                            logger.LogError(500,
                                exceptionHandlerFeature.Error,
                                exceptionHandlerFeature.Error.Message);
                        }
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });
            }
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(c =>
            {
                c.DefaultModelsExpandDepth(-1);
                c.SwaggerEndpoint($"v1/swagger.json", "Chem Website");
                c.RoutePrefix = "swagger";
            });
            app.UseStaticFiles();

            app.UseHangfireDashboard();

            //app.UseHangfireServer(new BackgroundJobServerOptions
            //{
            //    Queues = QueuePriority.Priority
            //});


            app.UseCors("ExposeResponseHeaders");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<UserHub>("/userHub");
                endpoints.MapHangfireDashboard();
            });
            jobService.StartScheduler();
            SpaStartup.Configure(app);
        }

        public JwtSettings GetJwtSettings()
        {
            JwtSettings settings = new JwtSettings();

            settings.Key = Configuration["JwtSettings:key"];
            settings.Audience = Configuration["JwtSettings:audience"];
            settings.Issuer = Configuration["JwtSettings:issuer"];
            settings.MinutesToExpiration =
             Convert.ToInt32(
                Configuration["JwtSettings:minutesToExpiration"]);

            return settings;
        }
    }
}
