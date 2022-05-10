using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using EShopApi.Interfaces;
using EShopApi.Models;
using EShopApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace EShopApi
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EShopApi", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(Directory.GetCurrentDirectory(), @"bin\Debug\net5.0", "EShopApi.xml"));
            });

            services.AddDbContext<EShopApi_DBContext>(options =>
            {
                options.UseSqlServer(
                    "Data Source=192.168.12.3;Initial Catalog=EShopApi_DB;Persist Security Info=True;User ID=sa;Password=Lemon7433");
            });
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ISalesPersonsRepository, SalesPersonsRepository>();
            services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "http://localhost:38469",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ourVerifyToplearn"))
                    };
                }
            );
            services.AddCors(options =>
            {
                 options.AddPolicy("EnableCors", builder =>
                 {
                     builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build();
                 });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EShopApi v1"));
            }

            app.UseResponseCaching();
            app.UseRouting();

            app.UseAuthorization();
            app.UseCors("EnableCors");
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
