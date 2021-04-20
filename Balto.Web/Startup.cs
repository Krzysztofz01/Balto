using Balto.Repository;
using Balto.Repository.Context;
using Balto.Service;
using Balto.Service.Settings;
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
            //Automapper service setup
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
            });

            //Datebase configuration
            services.AddDbContext<BaltoDbContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionStringSql")));

            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IObjectiveRepository, ObjectiveRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();

            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IObjectiveService, ObjectiveService>();
            services.AddScoped<INoteService, NoteService>();

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

            //Controllers
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(cfg =>
            {
                cfg.SwaggerEndpoint("/swagger/v1/swagger.json", "Balto WebAPI v1");
            });
            
            app.UseCors("DefaultPolicy");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
