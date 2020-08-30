using ITCraftTask.BusinessLogicLayer.Interfaces;
using ITCraftTask.BusinessLogicLayer.Services;
using ITCraftTask.DataAccessLayer.AppContext;
using ITCraftTask.DataAccessLayer.Entities;
using ITCraftTask.DataAccessLayer.Interfaces;
using ITCraftTask.DataAccessLayer.Repositories;
using ITCraftTask.Presentation.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace ITCraftTask.Presentation
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
            services.AddControllers();
            services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, Role>(options =>
            {
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
            })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IJwtHelper, JwtHelper>();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(BusinessLogicLayer.Constants.JwtConstants.SigningSecurityKey)),
                        ValidateIssuer = true,
                        ValidIssuer = BusinessLogicLayer.Constants.JwtConstants.ValidIssuer,
                        ValidateAudience = true,
                        ValidAudience = BusinessLogicLayer.Constants.JwtConstants.ValidAudience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                })
                .AddCookie();

            services.AddCors(
            options =>
            {
                options.AddPolicy("OriginPolicy",
                    policy => policy.WithOrigins("Origins")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowCredentials().AllowAnyHeader().AllowAnyMethod());
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
