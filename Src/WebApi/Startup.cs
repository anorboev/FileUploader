using Application.Implementation;
using Application.Interfaces;
using AutoMapper;
using Domain.Common.ValidationAttributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using WebApi.Helpers;

namespace WebApi
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
            services.AddAutoMapper(typeof(Startup));
            services.Configure<FileSettings>(Configuration.GetSection("FileSettings"));

            services.AddDbContext<FileUploaderDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

            services.AddScoped<IFileUploaderDbContext>(provider => provider.GetService<FileUploaderDbContext>());
            services.AddScoped<IFileService, FileService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowAll");

            app.ConfigureExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
