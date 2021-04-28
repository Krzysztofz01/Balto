using Balto.Repository;
using Balto.Repository.Context;
using Balto.Service;
using Balto.Service.Settings;
using Balto.Service.Integration.Trello;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Hangfire;
using Hangfire.MemoryStorage;
using System;

namespace Balto.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Automapper
            services.AddAutoMapper(cfg =>
            {
                //Service layer, Domain models to Dtos
                cfg.AddProfile<Service.Profiles.NoteProfile>();
                cfg.AddProfile<Service.Profiles.ObjectiveProfile>();
                cfg.AddProfile<Service.Profiles.ProjectProfile>();
                cfg.AddProfile<Service.Profiles.ProjectTableProfile>();
                cfg.AddProfile<Service.Profiles.ProjectTableEntryProfile>();
                cfg.AddProfile<Service.Profiles.UserProfile>();

                //Web layer, Dtos to view models
                cfg.AddProfile<Web.Profiles.ObjectiveViewProfile>();
                cfg.AddProfile<Web.Profiles.NoteViewProfile>();
                cfg.AddProfile<Web.Profiles.ProjectViewProfile>();
                cfg.AddProfile<Web.Profiles.ProjectTableViewProfile>();
                cfg.AddProfile<Web.Profiles.ProjectTableEntryViewProfile>();
            });

            //Datebase configuration
            services.AddDbContext<BaltoDbContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionStringSql")));

            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IObjectiveRepository, ObjectiveRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<INoteReadWriteUserRepository, NoteReadWriteUserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectTableRepository, ProjectTableRepository>();
            services.AddScoped<IProjectTableEntryRepository, ProjectTableEntryRepository>();
            services.AddScoped<IProjectReadWriteUserRepository, ProjectReadWriteUserRepository>();

            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IObjectiveService, ObjectiveService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectTableService, ProjectTableService>();
            services.AddScoped<IProjectTableEntryService, ProjectTableEntryService>();

            services.AddScoped<ITrelloIntegrationService, TrelloIntegrationService>();

            //Cross-Origin Resource Sharing
            services.AddCors(o => o.AddPolicy("DefaultPolicy", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            }));

            //JWT Authentication
            var jwtSettingsSection = Configuration.GetSection(nameof(JWTSettings));
            services.Configure<JWTSettings>(jwtSettingsSection);

            var jwtSettings = jwtSettingsSection.Get<JWTSettings>();
            var secret = Encoding.ASCII.GetBytes(jwtSettings.TokenSecret);

            services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //Endpoint versioning
            services.AddApiVersioning(opt =>
            {
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
            });

            //Swagger
            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new OpenApiInfo { Title = "Balto WebAPI", Version = "v1" });
            });

            //Hangfire
            services.AddHangfire(cfg =>
            {
                cfg.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseDefaultTypeSerializer()
                    .UseMemoryStorage();
            });

            //Controllers
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRecurringJobManager recurringJobManager, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("DefaultPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(cfg =>
            {
                cfg.SwaggerEndpoint("/swagger/v1/swagger.json", "Balto WebAPI v1");
            });

            app.UseHangfireServer();

            recurringJobManager.AddOrUpdate("Daily objective reset", () => serviceProvider.GetService<IObjectiveService>().ResetDaily(), Cron.Daily);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });    
        }
    }
}
