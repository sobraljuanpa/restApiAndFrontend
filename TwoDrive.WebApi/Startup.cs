using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwoDrive.BusinessLogic;
using TwoDrive.DataAccess.Interface;
using TwoDrive.DataAccess;
using TwoDrive.Domain;
using TwoDrive.BusinessLogic.Interface;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace TwoDrive.WebApi
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
            services.AddDbContext<TwoDriveContext>(opts => opts.UseSqlServer(
                    Configuration["ConnectionString:TwoDriveDB"],
                    b => b.MigrationsAssembly("TwoDrive.WebApi")));
            services.AddScoped<IDataRepository<User>, UserRepository>();
            services.AddScoped<IDataRepository<Folder>, FolderRepository>();
            services.AddScoped<IDataRepository<File>, FileRepository>();
            services.AddScoped<ILogic<User>, UserLogic>();
            services.AddScoped<FolderElementLogic<File>, FileLogic>();
            services.AddScoped<FolderElementLogic<Folder>, FolderLogic>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //esto es lo de autenticacion
            var key = Encoding.ASCII.GetBytes("Ahora este es nuestro secreto.");
            services.AddAuthentication(x =>
           {
               x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
