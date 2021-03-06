﻿using Microsoft.AspNetCore.Builder;
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
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System.Buffers;

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
            services.AddScoped<IDataRepository<LogItem>, LogRepository>();
            services.AddScoped<ILogic<User>, UserLogic>();
            services.AddScoped<IReport<File>, ReportLogic>(); 
            services.AddScoped<FolderElementLogic<File>, FileLogic>();
            services.AddScoped<FolderElementLogic<Folder>, FolderLogic>();
            services.AddMvc(options =>
            {
                options.OutputFormatters.Clear();
                options.OutputFormatters.Add(new JsonOutputFormatter(new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                }, ArrayPool<char>.Shared));
            }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TwoDrive API", Version = "v1" });
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TwoDrive API V1");
                c.RoutePrefix = string.Empty;
            });

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
