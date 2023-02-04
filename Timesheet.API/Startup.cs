using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.App.Services;
using Timesheet.Domain.Interfaces;

namespace Timesheet.API
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
            /*
             * services.AddTransient<IRepository, Repository>();
             * services.AddScoped<IBService, BService>();
             * services.AddScoped<IAService, AService>();
             * 
             * Repository ������������ � ���� �������� � ���� � ��� Transient, �� ��� ������� ������� �����
             * ������ ���� ������ ���� IRepository
             * � ���� �� ��� AddScoped, �� ���� ������ ����������� ������������� �� ��� ���� ��������
             */

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITimesheetService, TimesheetService>();
            services.AddSingleton<UserSession>();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
