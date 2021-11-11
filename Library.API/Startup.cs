using Library.BL.Services.Interfaces;
using Library.BL.Services.Providers;
using Library.DL.Services;
using Library.DL.Services.Interfaces;
using Library.DL.Services.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.API.Config;
using Library.Models;

namespace Library.API
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
            services.AddAutoMapper(typeof(AuthorProfile));
            services.AddAutoMapper(typeof(BookProfile));
            services.AddAutoMapper(typeof(PublisherProfile));

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAuthorProvider, AuthorProvider>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookProvider, BookProvider>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IPublisherProvider, PublisherProvider>();

            services.AddDbContext<DataBaseContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
