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
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Hangfire;
using Hangfire.MemoryStorage;
using System;
using System.Text;

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
            //Settings
            services.Configure<LeaderSettings>(Configuration.GetSection(nameof(LeaderSettings)));
            services.Configure<SMTPSettings>(Configuration.GetSection(nameof(SMTPSettings)));

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
                cfg.AddProfile<Service.Profiles.TeamProfile>();

                //Web layer, Dtos to view models
                cfg.AddProfile<Web.Profiles.ObjectiveViewProfile>();
                cfg.AddProfile<Web.Profiles.NoteViewProfile>();
                cfg.AddProfile<Web.Profiles.ProjectViewProfile>();
                cfg.AddProfile<Web.Profiles.ProjectTableViewProfile>();
                cfg.AddProfile<Web.Profiles.ProjectTableEntryViewProfile>();
                cfg.AddProfile<Web.Profiles.UserViewProfile>();
                cfg.AddProfile<Web.Profiles.TeamViewProfile>();
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
            services.AddScoped<ITeamRepository, TeamRepository>();

            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IObjectiveService, ObjectiveService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectTableService, ProjectTableService>();
            services.AddScoped<IProjectTableEntryService, ProjectTableEntryService>();
            services.AddScoped<ITeamService, TeamService>();

            services.AddScoped<ITrelloIntegrationService, TrelloIntegrationService>();
            services.AddScoped<IEmailService, EmailService>();

            //Cross-Origin Resource Sharing
            services.AddCors(o => o.AddPolicy("DefaultPolicy", builder =>
            {
                builder.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(o => true).AllowCredentials();
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
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
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

            //Hangfire background jobs
            recurringJobManager.AddOrUpdate("Daily objective reset",
                () => serviceProvider.GetService<IObjectiveService>().ResetDaily(), Cron.Daily);
            
            recurringJobManager.AddOrUpdate("Delete old objectives",
                () => serviceProvider.GetService<IObjectiveService>().DeleteOldFinished(), Cron.Daily);

            recurringJobManager.AddOrUpdate("Email incoming objectives - day",
                () => serviceProvider.GetService<IEmailService>().ObjectiveReminderDay(), Cron.Daily);

            recurringJobManager.AddOrUpdate("Email incoming objectives - week",
                () => serviceProvider.GetService<IEmailService>().ObjectiveReminderWeek(), Cron.Daily);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });    
        }
    }
}
